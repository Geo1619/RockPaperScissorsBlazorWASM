using RockPaperScissors.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace RockPaperScissors.Pages
{
    public partial class Index : IDisposable
    {

        List<Hand> Hands = new List<Hand>
        {
            new Hand{Selection = OptionRPS.Paper, WinsAgainst = OptionRPS.Rock, LosesAgainst = OptionRPS.Scissors, Image = "paper.jpg"},
            new Hand{Selection = OptionRPS.Rock, WinsAgainst = OptionRPS.Scissors, LosesAgainst = OptionRPS.Paper, Image = "rock.jpg"},
            new Hand{Selection = OptionRPS.Scissors, WinsAgainst = OptionRPS.Paper, LosesAgainst = OptionRPS.Rock, Image = "scissors.jpg"}
        };

        Timer timer;
        Hand opponentHand;
        string resultMessage = string.Empty;
        string resultMessageColor = string.Empty;
        protected override void OnInitialized()
        {
            opponentHand = Hands[0];
            timer = new Timer();
            timer.Interval = 200;
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
        }
        int indexOpponentHand = 0;
        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            indexOpponentHand = (indexOpponentHand + 1) % Hands.Count;
            opponentHand = Hands[indexOpponentHand];
            StateHasChanged();
        }

        private void SelectHand(Hand hand)
        {
            timer.Stop();
            var result =  hand.PlayAgainst(opponentHand);
            switch (result)
            {
                case GameStatus.Victory:
                    resultMessage = "You won!";
                    resultMessageColor = "green";
                    break;
                case GameStatus.Loss:
                    resultMessage = "You lost!";
                    resultMessageColor = "red";
                    break;
                default:
                    resultMessage = "It's a draw!";
                    resultMessageColor = "black";
                    break;
            }
        }
        private void PlayAgain()
        {
            timer.Start();
            resultMessage = string.Empty;
        }

        public void Dispose()
        {
            if (timer != null)
            {
                timer.Dispose();
            }
        }
    }
}
