using System;
using System.Collections.Generic;
using GrammarLibrary;
using StandardLibrary;
using WorldModel;

namespace ParserLibrary
{
    public class Parser
    {
        private Grammar _grammar;
        private World _world;

        public Parser(Grammar grammar, World world)
        {
            _grammar = grammar;
            _world = world;
        }

        /// <summary>
        /// Get list of words from input
        /// Tokenize list of words
        /// Check if first word is a known verb
        /// Get grammar sentence that matches the tokens
        /// Validate tokens in World Model
        /// Execute grammar action
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ParserResults Parse(string input)
        {
            ParserResults results = new ParserResults();

            List<string> words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            List<Token> tokens = TokenizeInput(words);

            // TO DO - find delegate in grammar that matches the tokens

            return results;
        }

        public List<Token> TokenizeInput(List<string> words)
        {
            List<Token> tokens = new List<Token>();
            int nounCount = 0;

            for (int i = 0; i < words.Count; i++)
            {
                var word = words[i];

                if (i == 0) // First word should be a verb
                {
                    if (IsVerb(word))
                    {
                        tokens.Add(new Token(TokenType.Verb, word));
                    }
                    else
                    {
                        tokens.Clear();
                        return tokens;
                    }
                }
                else // Subsequent words
                {
                    if (i < words.Count - 1 && IsAdjective(word) && IsNoun(words[i + 1]))
                    {
                        tokens.Add(new Token(TokenType.Adjective, word));
                        i++; // Increment the loop counter to skip the next word
                        word = words[i]; // Update the current word to the noun
                    }

                    if (IsNoun(word))
                    {
                        TokenType nounTokenType;

                        if (nounCount == 0)
                        {
                            nounTokenType = TokenType.Noun;
                        }
                        else if (nounCount == 1)
                        {
                            nounTokenType = TokenType.Second;
                        }
                        else
                        {
                            nounTokenType = TokenType.Third;
                        }

                        tokens.Add(new Token(nounTokenType, word));
                        nounCount++;
                    }
                    else if (IsArticle(word))
                    {
                        tokens.Add(new Token(TokenType.Article, word));
                    }
                    else if (IsPreposition(word))
                    {
                        tokens.Add(new Token(TokenType.Preposition, word));
                    }
                    else
                    {
                        tokens.Clear();
                        return tokens;
                    }
                }
            }

            return tokens;
        }

        private bool IsNoun(string word)
        {
            return _world.Nodes
                .Where(pair => pair.Value.Data is IThing)
                .Any(pair => ((IThing)pair.Value.Data).Name.Equals(word, StringComparison.OrdinalIgnoreCase));
        }

        private bool IsVerb(string word)
        {
            return _grammar.Sentences.Keys
                .Any(verb => verb.Equals(word, StringComparison.OrdinalIgnoreCase));
        }

        private bool IsPreposition(string word)
        {
            return word == "in" || word == "on";
        }

        private bool IsArticle(string word)
        {
            return word == "a" || word == "an" || word == "the";
        }

        private bool IsAdjective(string word)
        {
            return _grammar.Sentences.ContainsKey(word);
        }

    }
}
