using System;
using System.Collections.Generic;
using System.Linq;
using WorldModel;

namespace GrammarLibrary
{
    /// <summary>
    /// Eventually we want to be able to handle:
    ///     pot pot plant in plant pot
    ///     verb adjective noun in adjective noun
    /// </summary>
    public class Grammar
    {
        private List<Token> _components;
        private Dictionary<string, List<List<Token>>> _sentences;
        private World _world;

        public Grammar(World world)
        {
            _components = new List<Token>();
            _sentences = new Dictionary<string, List<List<Token>>>();

            _world = world;
        }

        /// <summary>
        /// We get a list of tokens from the parser. We need to find the sentence
        /// that matches the tokens.
        /// Then we need to validate the object portions with the World Model.
        /// If that succeeds, execute the associated grammar action.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public bool Validate(List<Token> tokens)
        {
            return true;
        }

        public Grammar Verb(string verbs, Action<List<Token>?>? action = null)
        {
            return AddVerb(verbs, action, ActionType.Standard);
        }

        private Grammar AddVerb(string verbs, Action<List<Token>?>? action, ActionType actionType)
        {
            var verbList = verbs.Split('/');
            foreach (var verb in verbList)
            {
                _components.Add(new Token(TokenType.Verb, verb, action, actionType));
            }
            return this;
        }

        public Grammar MetaVerb(string verb, Action<List<Token>?>? action = null)
        {
            return AddVerb(verb, action, ActionType.Meta);
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

        public void OverrideActionDelegate(string verb, List<Token> sentence, Action<List<Token>?> newAction)
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
        Second,
        Third,
        Adjective,
        Article,
        Preposition
    }

    public class Token
    {
        public TokenType Type { get; }
        public string? Value { get; }
        public Action<List<Token>?>? Delegate { get; set; }
        public ActionType? ActionType { get; }

        public Token(TokenType type, string? value = null, Action<List<Token>?>? actionDelegate = null, ActionType? actionType = GrammarLibrary.ActionType.None)
        {
            Type = type;
            Value = value;
            Delegate = actionDelegate;
            ActionType = actionType;
        }
    }

}
