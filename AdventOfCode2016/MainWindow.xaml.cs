using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdventOfCode2016
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string INSTRUCTION_FOLDER = "Instructions";
        private const string INPUT_FOLDER = "Input";
        private const string TEXT_FILE_PATTERN = "*.txt";
        private BackgroundWorker worker = new BackgroundWorker();
        
        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            // setup to create challenge values
            Dictionary<string, Challenge> challenges = new Dictionary<string, Challenge>();
            string basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);                        
            DirectoryInfo instructionDir = new DirectoryInfo(System.IO.Path.Combine(basePath, INSTRUCTION_FOLDER));
            DirectoryInfo inputDir = new DirectoryInfo(System.IO.Path.Combine(basePath, INPUT_FOLDER));
            FileInfo[] instructionFiles = instructionDir.GetFiles(TEXT_FILE_PATTERN, SearchOption.TopDirectoryOnly);
            FileInfo[] inputFiles = inputDir.GetFiles(TEXT_FILE_PATTERN, SearchOption.TopDirectoryOnly);                      
            // populate challenges
            for (int i = 1; i <= 25; i++)
            {
                string text = String.Format("Day {0}", i);
                Challenge challenge = getChallengeValue(i, instructionFiles, inputFiles);
                challenges.Add(text, challenge);                
            }
            // assign combobox source
            comboBoxChallenges.ItemsSource = challenges;
            comboBoxChallenges.DisplayMemberPath = "Key";
            comboBoxChallenges.SelectedValuePath = "Value";
            // setup worker
            this.worker.DoWork += worker_DoWork;
            this.worker.RunWorkerCompleted += worker_Completed;
        }

        private Challenge getChallengeValue(int day, FileInfo[] instructionFiles, FileInfo[] inputFiles)
        {
            // set the expected values
            string input = String.Format("InputDay{0}.txt", day.ToString("00"));
            string instructions = String.Format("InstructionsDay{0}.txt", day.ToString("00"));
            FileInfo inputFile = inputFiles.Where(f => f.Name == input).FirstOrDefault();
            FileInfo instructionsFile = instructionFiles.Where(f => f.Name == instructions).FirstOrDefault();            
            // build and return the challenge value
            Challenge challenge = new Challenge(instructionsFile, inputFile, day);
            return challenge;
        }

        private void challengeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // get the value of the selected challenge
            Challenge challenge = (Challenge)comboBoxChallenges.SelectedValue;
            textboxPart1Answer.Text = String.Empty;
            textBoxPart1RunTime.Text = String.Empty;
            textBoxPart2Answer.Text = String.Empty;
            textBoxPart2RunTime.Text = String.Empty;
            // check if the selected challenge has input
            if (challenge.Input == null)
            {
                textBoxInput.Text = String.Empty;
            }
            else
            {
                // populate instruction text
                string inputText = File.ReadAllText(challenge.Input.FullName);
                textBoxInput.Text = inputText;
            }
            // check if the selected challenge has instructions
            if (challenge.Instructions == null)
            {
                // set default message
                string msg = "No instructions found.";
                textBoxInstructions.Text = String.Empty;
                // try and update the message to include the selected challenge day
                if (comboBoxChallenges.SelectedItem.GetType().Equals(typeof(KeyValuePair<string, Challenge>)))
                {
                    KeyValuePair<string, Challenge> kvp = (KeyValuePair<string, Challenge>)comboBoxChallenges.SelectedItem;
                    msg = String.Format("No instructions found for {0}.", kvp.Key.ToLower());
                }
                // inform user of missing instructions
                MessageBox.Show(msg);
            }
            else
            {
                // populate instruction text
                string instructionText = File.ReadAllText(challenge.Instructions.FullName);
                textBoxInstructions.Text = instructionText;                               
            }
        }

        private void buttonRun_Clicked(object sender, RoutedEventArgs e)
        {
            if (comboBoxChallenges.SelectedValue == null)
            {
                MessageBox.Show("Please select a challenge to run.");
            }
            else
            {
                // get the value of the selected challenge
                Challenge challenge = (Challenge)comboBoxChallenges.SelectedValue;
                comboBoxChallenges.IsEnabled = false;
                progressBarMain.IsIndeterminate = true;
                worker.RunWorkerAsync(challenge);
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // unbox challenge
            Challenge challenge = (Challenge)e.Argument;
            // run part 1
            Stopwatch timer = new Stopwatch();
            timer.Start();
            string part1Answer = challenge.GetPart1Answer();
            timer.Stop();
            string part1RunTime = timer.Elapsed.ToString("mm\\:ss");
            // run part 2
            timer = new Stopwatch();
            timer.Start();
            string part2Answer = challenge.GetPart2Answer();
            timer.Stop();
            string part2RunTime = timer.Elapsed.ToString("mm\\:ss");
            // box the result
            KeyValuePair<string, string> part1 = new KeyValuePair<string, string>(part1Answer, part1RunTime);
            KeyValuePair<string, string> part2 = new KeyValuePair<string, string>(part2Answer, part2RunTime);
            Tuple<KeyValuePair<string, string>, KeyValuePair<string, string>> result = new Tuple<KeyValuePair<string, string>, KeyValuePair<string, string>>(part1, part2);
            e.Result = result;
        }
        
        private void worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Tuple<KeyValuePair<string, string>, KeyValuePair<string, string>> result = (Tuple<KeyValuePair<string, string>, KeyValuePair<string, string>>)e.Result;
            textboxPart1Answer.Text = result.Item1.Key;
            textBoxPart1RunTime.Text = result.Item1.Value;
            textBoxPart2Answer.Text = result.Item2.Key;
            textBoxPart2RunTime.Text = result.Item2.Value;
            comboBoxChallenges.IsEnabled = true;
            progressBarMain.IsIndeterminate = false;
        }
    }
}
