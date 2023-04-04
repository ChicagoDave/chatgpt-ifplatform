using System;
using System.Collections.Generic;
using GrammarLibrary;

namespace ParserLibrary
{

    namespace ParserLibrary
    {
        public class Parser
        {
            private Grammar _grammar;

            public Parser(Grammar grammar)
            {
                _grammar = grammar;
            }

            public void Parse(string input)
            {
                List<string> words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                List<Token> tokens = new List<Token>();
                ActionType actionType = ActionType.None;

                foreach (string word in words)
                {
                    if (IsVerb(word, out Action? action))
                    {
                        tokens.Add(new Token(TokenType.Verb, word, action, actionType));
                        actionType = ActionType.None;
                    }
                    else if (_grammar.Sentences.ContainsKey(word))
                    {
                        tokens.Add(new Token(TokenType.Noun, word, null, actionType));
                        actionType = ActionType.None;
                    }
                    else if (IsPreposition(word))
                    {
                        tokens.Add(new Token(TokenType.Preposition, word, null, actionType));
                        actionType = ActionType.None;
                    }
                    else if (IsArticle(word))
                    {
                        actionType = ActionType.Standard;
                    }
                    else if (IsAdjective(word))
                    {
                        tokens.Add(new Token(TokenType.Adjective, word, null, actionType));
                    }
                }

                ExecuteAction(tokens);
            }

            private bool IsVerb(string word, out Action? action)
            {
                action = null;
                if (_grammar.Sentences.ContainsKey(word))
                {
                    var sentence = _grammar.Sentences[word][0];

                    foreach (var token in sentence)
                    {
                        if (token.Type == TokenType.Verb)
                        {
                            action = token.Delegate;
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

            private void ExecuteAction(List<Token> tokens)
            {
                foreach (var token in tokens)
                {
                    if (token.Delegate != null)
                    {
                        token.Delegate();
                        break;
                    }
                }
            }
        }
    }

}
