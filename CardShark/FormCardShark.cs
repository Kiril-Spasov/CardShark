using System;
using System.IO;
using System.Windows.Forms;

namespace CardShark
{
    public partial class FormCardShark : Form
    {
        public FormCardShark()
        {
            InitializeComponent();
        }

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            string line = "";
            string[] input = new string[7];
            int[] cards = new int[7];

            string path = Application.StartupPath + @"\cards.txt";
            StreamReader streamReader = new StreamReader(path);

            int linesCount = Convert.ToInt32(streamReader.ReadLine());

            for (int i = 1; i <= linesCount; i++)
            {
                line = streamReader.ReadLine();
                input = line.Split(' ');

                for (int j = 0; j < input.Length; j++)
                {
                    cards[j] = Convert.ToInt32(input[j]);
                }

                TxtResult.Text += CheckHand(cards) ? "Bet" : "Fold";
                TxtResult.Text += Environment.NewLine;
            }
        }

        private bool CheckHand(int[] cards)
        {
            bool bet = false;
            int[] left5Cards = new int[5];

            //We sort the cards and discard the 2 highest cards.
            Sort(cards);
            for (int i = 0; i < 5; i++)
            {
                left5Cards[i] = cards[i];
            }

            if (CheckForDuplicates(left5Cards))
            {
                bet = false;
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (left5Cards[i] == i + 1)
                    {
                        bet = true;
                    }
                    else
                    {
                        bet = false;
                    }
                }
            }
            return bet;
        }

        private void Sort(int[] numbers)
        {
            bool flag;
            int temp;

            do
            {
                flag = false;

                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    if (numbers[i] > numbers[i + 1])
                    {
                        temp = numbers[i];
                        numbers[i] = numbers[i + 1];
                        numbers[i + 1] = temp;
                        flag = true;
                    }
                }
            }
            while (flag != false);
        }

        private bool CheckForDuplicates(int[] numbers)
        {
            bool duplicates = false;

            int[] frequency = new int[14];

            for (int i = 0; i < numbers.Length; i++)
            {
                frequency[numbers[i]] = frequency[numbers[i]] + 1;
            }

            for (int i = 0; i < frequency.Length; i++)
            {
                if (frequency[i] > 1)
                    duplicates = true;
            }
            return duplicates;
        }
    }
}
