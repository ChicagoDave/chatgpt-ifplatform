using System;
using System.Collections.Generic;
using GrammarLibrary;

namespace ParserLibrary
{
    public class Parser
    {
        private Grammar _grammar;

        public Parser(Grammar grammar)
        {
            _grammar = grammar;
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
            List<Token> tokens = new List<Token>();

            int nounCount = 0;
            int wordCount = 0;

            bool isVerb = IsVerb(words[wordCount]);

            if (isVerb)
            {
                var potentialSentences = _grammar.Sentences[words[wordCount]].ToList<List<Token>>();

                if (potentialSentences == null)
                    throw new Exception("No sentences found for verb.");

                List<Token> firstSentence = potentialSentences[0];
                var actionType = firstSentence[0].ActionType;
                var action = firstSentence[0].Delegate;

                // If it's a meta verb, we're done parsing
                if (actionType == ActionType.Meta)
                {
                    if (action == null)
                        throw new Exception("Meta verb action is null.");
                    else
                        results.Action = action;

                    results.Tokens = tokens;
                    results.Success = true;
                    return results;
                } else
                {
                    // standard action, build token list
                    wordCount++;

                    for (int nextWord=wordCount; nextWord < words.Count; nextWord++)
                    {
                        // now we need to determine if the next word is one of:
                        // noun, adjective, article, preposition
                        // nouns and adjectives will be in the world model
                        // articles and prepositions will be in the grammar sentence
                    }
                }
            } else
            {
                results.Success = false;
                results.Errors.Add("Invalid statement.");
            }

            // First tokenize the user input
            //foreach (string word in words)
            //{
            //    if (IsVerb(word, out Action? action))
            //    {
            //        tokens.Add(new Token(TokenType.Verb, word, action, actionType));
            //        actionType = ActionType.None;
            //    }
            //    else if (_grammar.Sentences.ContainsKey(word))
            //    {
            //        tokens.Add(new Token(TokenType.Noun, word, null, actionType));
            //        actionType = ActionType.None;
            //    }
            //    else if (IsPreposition(word))
            //    {
            //        tokens.Add(new Token(TokenType.Preposition, word, null, actionType));
            //        actionType = ActionType.None;
            //    }
            //    else if (IsArticle(word))
            //    {
            //        actionType = ActionType.Standard;
            //    }
            //    else if (IsAdjective(word))
            //    {
            //        tokens.Add(new Token(TokenType.Adjective, word, null, actionType));
            //    }
            //}

            // Now check the world model

            return results;
        }

        private bool IsVerb(string word)
        {
            if (_grammar.Sentences.ContainsKey(word))
            {
                var sentence = _grammar.Sentences[word][0];

                foreach (var token in sentence)
                {
                    if (token.Type == TokenType.Verb)
                    {
                        return true;
                    }
                }
            }

            return false;
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
