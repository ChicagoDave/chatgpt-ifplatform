using System;
using System.Collections.Generic;
using System.Linq;

namespace GrammarLibrary
{
    public class Grammar
    {
        private List<Token> _components;
        private Dictionary<string, List<List<Token>>> _sentences;

        public Grammar()
        {
            _components = new List<Token>();
            _sentences = new Dictionary<string, List<List<Token>>>();
        }

        public Grammar Verb(string verb, Action? action = null)
        {
            return AddVerb(verb, action, ActionType.Standard);
        }

        public Grammar MetaVerb(string verb, Action? action = null)
        {
            return AddVerb(verb, action, ActionType.Meta);
        }

        private Grammar AddVerb(string verb, Action? action, ActionType actionType)
        {
            _components.Add(new Token(TokenType.Verb, verb, action, actionType));
            return this;
        }

        public Grammar Noun => AddToken(TokenType.Noun);

        public Grammar Adjective => AddToken(TokenType.Adjective);

        public Grammar A => AddToken(TokenType.Article, "a");

        public Grammar An => AddToken(TokenType.Article, "an");

        public Grammar The => AddToken(TokenType.Article, "the");

        public Grammar In => AddToken(TokenType.Preposition, "in");

        public Grammar On => AddToken(TokenType.Preposition, "on");

        // Add more prepositions as needed...

        private Grammar AddToken(TokenType type, string? value = null)
        {
            _components.Add(new Token(type, value));
            return this;
        }

        public Grammar Continue
        {
            get
            {
                StoreSentence();
                return this;
            }
        }

        public string End
        {
            get
            {
                StoreSentence();
                return "End";
            }
        }

        private void StoreSentence()
        {
            var verbToken = _components.First(t => t.Type == TokenType.Verb);
            string? verb = verbToken.Value;

            if (verb != null)
            {
                if (!_sentences.ContainsKey(verb))
                {
                    _sentences[verb] = new List<List<Token>>();
                }

                _sentences[verb].Add(new List<Token>(_components));
            }

            _components.Clear();
        }

        public void OverrideActionDelegate(string verb, List<Token> sentence, Action newAction)
        {
            if (_sentences.ContainsKey(verb))
            {
                var matchingSentence = _sentences[verb].FirstOrDefault(s => s.SequenceEqual(sentence));
                if (matchingSentence != null)
                {
                    var verbToken = matchingSentence.First(t => t.Type == TokenType.Verb);
                    verbToken.Delegate = newAction;
                }
            }
        }

        public List<Token>? FindSentence(string verb, Predicate<List<Token>> match)
        {
            if (_sentences.ContainsKey(verb))
            {
                return _sentences[verb].FirstOrDefault(s => match(s));
            }
            return null;
        }



        public IReadOnlyDictionary<string, List<List<Token>>> Sentences => _sentences;
    }

    public enum ActionType
    {
        None,
        Standard,
        Meta
    }

    public enum TokenType
    {
        Verb,
        Noun,
        Adjective,
        Article,
        Preposition
    }

    public class Token
    {
        public TokenType Type { get; }
        public string? Value { get; }
        public Action? Delegate { get; set; }
        public ActionType ActionType { get; }

        public Token(TokenType type, string? value = null, Action? actionDelegate = null, ActionType actionType = ActionType.None)
        {
            Type = type;
            Value = value;
            Delegate = actionDelegate;
            ActionType = actionType;
        }
    }

}
