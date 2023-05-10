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
            var standardPot = _grammar.Verb("pot").Adjective.Noun.In.Adjective.Noun.End;

            var standardTake = _grammar.Verb("take/get/grab", Take).Noun.End;
            var metaExamine = _grammar.Verb("examine/x/ex", Examine).Noun.End;
            var standardGoInNoun = _grammar.Verb("go", GoInNoun).In.Noun.End;
            var standardGoNorth = _grammar.Direction("north/n", GoNorth).End;
            var standardGoNortheast = _grammar.Direction("northeast/ne", GoNortheast).End;
            var standardGoEast = _grammar.Direction("east/e", GoEast).End;
            var standardGoSoutheast = _grammar.Direction("southeast/se", GoSoutheast).End;
            var standardGoSouth = _grammar.Direction("south/s", GoSouth).End;
            var standardGoSouthwest = _grammar.Direction("southwest/sw", GoSouthwest).End;
            var standardGoWest = _grammar.Direction("west/w", GoWest).End;
            var standardGoNorthwest = _grammar.Direction("northwest/nw", GoNorthwest).End;
            var standardGoIn = _grammar.Verb("in", GoIn).End;
            var standardGoInPrep = _grammar.Verb("go", GoIn).In.End;
            var standardGoOut = _grammar.Direction("out", GoOut).End;
            var standardGoUp = _grammar.Direction("up/u", GoUp).End;
            var standardGoDown = _grammar.Direction("down/d", GoDown).End;
            var standardWait = _grammar.Verb("wait/z", Wait).End;

            var standardHangOn = _grammar.Verb("hang", HangOn).On.Noun.End;
            var standardWear = _grammar.Verb("wear", Wear).Noun.End;
            var standardPutOn = _grammar.Verb("put", Wear).On.Noun.End;

            // Meta actions
            _grammar.MetaVerb("score", Score);
            _grammar.MetaVerb("restore", Restore);
            _grammar.MetaVerb("restart", Restart);
            _grammar.MetaVerb("i/inventory", Inventory);

            // Override the "go" action
            var goInNoun = new List<Token>
            {
                new Token(TokenType.Verb, "go", GoIn, ActionType.Standard),
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

        private void GoNorth(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }

        private void GoNortheast(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }
        private void GoEast(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }
        private void GoSoutheast(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }
        private void GoSouth(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }
        private void GoSouthwest(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }
        private void GoWest(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }
        private void GoNorthwest(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }
        private void GoInNoun(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }
        private void GoIn(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere.");
        }

        private void GoOut(List<Token>? tokens)
        {
            Console.WriteLine("You go in the thing.");
        }

        private void GoUp(List<Token>? tokens)
        {
            Console.WriteLine("You go in the thing.");
        }

        private void GoDown(List<Token>? tokens)
        {
            Console.WriteLine("You go in the thing.");
        }
        private void CustomGo(List<Token>? tokens)
        {
            Console.WriteLine("You go somewhere using a custom action.");
        }

        private void Wait(List<Token>? tokens)
        {
            Console.WriteLine("You contemplate the universe.");
        }

        private void HangOn(List<Token>? tokens)
        {
            Console.WriteLine("You contemplate the universe.");
        }

        private void Wear(List<Token>? tokens)
        {
            Console.WriteLine("You contemplate the universe.");
        }

        private void Inventory(List<Token>? tokens)
        {
            Console.WriteLine("Your score is 0.");
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
