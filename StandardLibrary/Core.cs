using System;
using System.Collections.Generic;
using GrammarLibrary;
using WorldModel;

namespace StandardLibrary
{
    public class Core
    {
        private Grammar _grammar;

        public WorldManager WorldManager { get; private set; }

        public Player Player { get; private set; }

        public Core(World world)
        {
            _grammar = new Grammar(world);
            WorldManager = new WorldManager(world);

            Player = WorldManager.InitializePlayer(Player);

            // Standard actions
            var standardTake = _grammar.Verb("take", Take).Noun.End;
            var metaExamine = _grammar.Verb("examine", Examine).Noun.End;
            var standardGoInNoun = _grammar.Verb("go", Go).In.Noun.End;
            var standardGoNoun = _grammar.Verb("go", Go).On.Noun.End;

            // Meta actions
            _grammar.MetaVerb("score", Score);
            _grammar.MetaVerb("restore", Restore);
            _grammar.MetaVerb("restart", Restart);

            // Override the "go" action
            var goInNoun = new List<Token>
            {
                new Token(TokenType.Verb, "go", Go, ActionType.Standard),
                new Token(TokenType.Preposition, "in"),
                new Token(TokenType.Noun)
            };

            _grammar.OverrideActionDelegate("go", goInNoun, CustomGo);
        }

        private void Take(List<Token>? tokens)
        {
            Console.WriteLine("You take the item.");
        }

        private void Examine(List<Token>? tokens)
        {
            Console.WriteLine("You examine the item.");
        }

        private void Go(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }

        private void CustomGo(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere using a custom action.");
        }

        private void Score(List<Token>? tokens)
        {
            Console.WriteLine("Your score is 0.");
        }

        private void Restore(List<Token>? tokens)
        {
            Console.WriteLine("Game restored.");
        }

        private void Restart(List<Token>? tokens)
        {
            Console.WriteLine("Game restarted.");
        }
    }
}
