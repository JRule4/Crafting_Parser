using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Crafting_Parser
{
    /// <summary>
    /// Parse logic.
    /// </summary>
    class CraftingParser
    {
        #region Counters
        /// <summary>
        /// Action Counters.
        /// </summary>
        public enum ActionCounters
        {
            UNUSED,

            //Basic Synthesis
            BasicSynth_Count,
            BasicSynth_Success,

            BasicSynth_SH0_Count,
            BasicSynth_SH0_Success,

            BasicSynth_SH1_Count,
            BasicSynth_SH1_Success,

            BasicSynth_SH2_Count,
            BasicSynth_SH2_Success,

            //Standard Synthesis
            StandardSynth_Count,
            StandardSynth_Success,

            StandardSynth_SH0_Count,
            StandardSynth_SH0_Success,

            StandardSynth_SH1_Count,
            StandardSynth_SH1_Success,

            StandardSynth_SH2_Count,
            StandardSynth_SH2_Success,

            //Rapid Synthesis
            RapidSynth_Count,
            RapidSynth_Success,

            RapidSynth_SH0_Count,
            RapidSynth_SH0_Success,

            RapidSynth_SH1_Count,
            RapidSynth_SH1_Success,

            RapidSynth_SH2_Count,
            RapidSynth_SH2_Success,

            //Basic Touch
            BasicTouch_Count,
            BasicTouch_Success,

            BasicTouch_SH0_Count,
            BasicTouch_SH0_Success,

            BasicTouch_SH1_Count,
            BasicTouch_SH1_Success,

            BasicTouch_SH2_Count,
            BasicTouch_SH2_Success,

            //Standard Touch
            StandardTouch_Count,
            StandardTouch_Success,

            StandardTouch_SH0_Count,
            StandardTouch_SH0_Success,

            StandardTouch_SH1_Count,
            StandardTouch_SH1_Success,

            StandardTouch_SH2_Count,
            StandardTouch_SH2_Success,

            //Advanced Touch
            AdvancedTouch_Count,
            AdvancedTouch_Success,

            AdvancedTouch_SH0_Count,
            AdvancedTouch_SH0_Success,

            AdvancedTouch_SH1_Count,
            AdvancedTouch_SH1_Success,

            AdvancedTouch_SH2_Count,
            AdvancedTouch_SH2_Success,

            //Hasty Touch
            HastyTouch_Count,
            HastyTouch_Success,

            HastyTouch_SH0_Count,
            HastyTouch_SH0_Success,

            HastyTouch_SH1_Count,
            HastyTouch_SH1_Success,

            HastyTouch_SH2_Count,
            HastyTouch_SH2_Success,

            //Byregot's Blessing
            ByregotsBlessing_Count,
            ByregotsBlessing_Success,

            ByregotsBlessing_SH0_Count,
            ByregotsBlessing_SH0_Success,

            ByregotsBlessing_SH1_Count,
            ByregotsBlessing_SH1_Success,

            ByregotsBlessing_SH2_Count,
            ByregotsBlessing_SH2_Success,

            //Non-Failable
            SH1_Count,
            SH2_Count,
            NQ_Crafts,
            HQ_Crafts,
            Failed_Crafts,
            Steps_Completed,
            Tricks_Used,
        }
        #endregion

        /// <summary>
        /// Action function.
        /// </summary>
        delegate void ActionFunction(string logLine, Action craftAction);

        /// <summary>
        /// Base Action.
        /// </summary>
        class Action
        {
            /// <summary>
            /// The counter associated with this action.
            /// </summary>
            public ActionCounters usesCounter = ActionCounters.UNUSED;
            /// <summary>
            /// The name of the action.
            /// </summary>
            public string actionName = "Unused";
            /// <summary>
            /// The log signerature.
            /// </summary>
            public string logSignerature = "Unused";
            /// <summary>
            /// The action function.
            /// </summary>
            public ActionFunction actionFunction;

            /// <summary>
            /// Initializes a new instance of the <see cref="Crafting_Parser.CraftingParser.Action"/> class.
            /// </summary>
            /// <param name='name'>
            /// Name.
            /// </param>
            /// <param name='signerature'>
            /// Signerature.
            /// </param>
            /// <param name='tries'>
            /// Tries.
            /// </param>
            /// <param name='func'>
            /// Func.
            /// </param>
            public Action(string name, string signerature, ActionCounters uses, ActionFunction func)
            {
                actionName = name;
                usesCounter = uses;
                logSignerature = signerature;
                actionFunction = func;
            }
        }

        /// <summary>
        /// Failable action.
        /// </summary>
        class FailableAction : Action
        {
            /// <summary>
            /// The success action counter.
            /// </summary>
            public ActionCounters successCounter = ActionCounters.UNUSED;

            /// <summary>
            /// Initializes a new instance of the <see cref="Crafting_Parser.CraftingParser.FailableAction"/> class.
            /// </summary>
            /// <param name='name'>
            /// Name.
            /// </param>
            /// <param name='signerature'>
            /// Signerature.
            /// </param>
            /// <param name='tries'>
            /// Try Counter.
            /// </param>
            /// <param name='successes'>
            /// Success Counter.
            /// </param>
            /// <param name='func'>
            /// Func.
            /// </param>
            public FailableAction(string name, string signerature, ActionCounters uses, ActionCounters successes, ActionFunction func)
                : base(name, signerature, uses, func)
            {
                successCounter = successes;
            }
        }

        /// <summary>
        /// The server name signerature.
        /// </summary>
        readonly string serverSignerature = "Welcome to ";
        /// <summary>
        /// The epoch time.
        /// </summary>
        readonly System.DateTime epochTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        /// <summary>
        /// The FF14 line endings.
        /// </summary>
        readonly string[] lineEndings = new string[] { "::", ":\x02" };
        /// <summary>
        /// The time stamp expression.
        /// </summary>
        readonly Regex timeStampExpression = new Regex(@"[A-F0-9]{12}:");
        /// <summary>
        /// The readable text expression.
        /// </summary>
        readonly Regex readableTextExpression = new Regex(@"[^\u001F-\u007F]");
        /// <summary>
        /// The tell signerature.
        /// This is used to find a character's name in the logs.
        /// </summary>
        readonly string tellSignerature = "\u0002\u0027\u0010\u0002\u0001\u0001\u0001\ufffd\u000a";
        /// <summary>
        /// The character folder signerature.
        /// </summary>
        readonly string characterFolderSignerature = "FFXIV_CHR";

        /// <summary>
        /// The total counters.
        /// </summary>
        Dictionary<ActionCounters, int> totalCounters = new Dictionary<ActionCounters, int>();
        /// <summary>
        /// The current craft counters.
        /// Reset upon starting/completing a craft.
        /// </summary>
        Dictionary<ActionCounters, int> currentCraftCounters = new Dictionary<ActionCounters, int>();
        /// <summary>
        /// The date nodes.
        /// </summary>
        Dictionary<string, TreeNode> dateNodes = new Dictionary<string, TreeNode>();
        /// <summary>
        /// The item type nodes.
        /// </summary>
        Dictionary<string, TreeNode> itemTypeNodes = new Dictionary<string, TreeNode>();
        /// <summary>
        /// The character logs.
        /// Each character's log directory.
        /// </summary>
        Dictionary<string, string> characterLogs = new Dictionary<string, string>();
        /// <summary>
        /// Gets the character logs.
        /// </summary>
        /// <value>
        /// The character logs.
        /// </value>
        public Dictionary<string, string> CharacterLogs
        {
            get { return characterLogs; }
        }

        /// <summary>
        /// The steady hand tracker.
        /// </summary>
        int steadyHand = 0;

        /// <summary>
        /// The item type tree view.
        /// </summary>
        TreeView itemTypeTreeView;
        /// <summary>
        /// The date tree view.
        /// </summary>
        TreeView dateTreeView;

        /// <summary>
        /// The craft actions.
        /// </summary>
        Action[] craftActions = null;

        /// <summary>
        /// If the parser should use DateTime filters
        /// </summary>
        bool useDateFilters = false;

        /// <summary>
        /// Starting time for the date filter
        /// </summary>
        DateTime startDateFilter;

        /// <summary>
        /// Ending time for the date filter
        /// </summary>
        DateTime endDateFilter;


        /// <summary>
        /// Init the specified dateView, itemView and ffCharactersPath.
        /// </summary>
        /// <param name='dateView'>
        /// Date view.
        /// </param>
        /// <param name='itemView'>
        /// Item view.
        /// </param>
        /// <param name='ffCharactersPath'>
        /// Ff characters path.
        /// </param>
        public void Init(TreeView dateView, TreeView itemView, string ffCharactersPath)
        {
            dateTreeView = dateView;
            itemTypeTreeView = itemView;

            //Set up actions.
            craftActions = new Action[] 
            { 
                new FailableAction("Basic Synthesis", "You use Basic Synth", ActionCounters.BasicSynth_Count, ActionCounters.BasicSynth_Success, IncreaseFailableCouter),
                new FailableAction("Standard Synthesis", "You use Standard Synth", ActionCounters.StandardSynth_Count, ActionCounters.StandardSynth_Success, IncreaseFailableCouter),
                new FailableAction("Rapid Synthesis", "You use Rapid Synth", ActionCounters.RapidSynth_Count, ActionCounters.RapidSynth_Success, IncreaseFailableCouter),
                new FailableAction("Basic Touch", "You use Basic Touch", ActionCounters.BasicTouch_Count, ActionCounters.BasicTouch_Success, IncreaseFailableCouter),
                new FailableAction("Standard Touch", "You use Standard Touch", ActionCounters.StandardTouch_Count, ActionCounters.StandardTouch_Success, IncreaseFailableCouter),
                new FailableAction("Advanced Touch", "You use Advanced Touch", ActionCounters.AdvancedTouch_Count, ActionCounters.AdvancedTouch_Success, IncreaseFailableCouter),
                new FailableAction("Hasty Touch", "You use Hasty Touch", ActionCounters.HastyTouch_Count, ActionCounters.HastyTouch_Success, IncreaseFailableCouter),
                new FailableAction("Byregot's Blessing", "You use Byregot's Blessing", ActionCounters.ByregotsBlessing_Count, ActionCounters.ByregotsBlessing_Success, IncreaseFailableCouter),
                new Action("Steps Completed", "You use", ActionCounters.Steps_Completed, CraftingStepCompleted),
                new Action("Tricks of the Trade", "You use Tricks of the Trade", ActionCounters.Tricks_Used, UsedTricks),
                new Action("", "   You gain the effect of Steady Hand.", ActionCounters.SH1_Count, GainSteadyHand),
                new Action("", "   You gain the effect of Steady Hand II.", ActionCounters.SH2_Count, GainSteadyHand),
                new Action("", "You lose the effect of Steady Hand", ActionCounters.UNUSED, LoseSteadyHand),
            };

            GetCharacterInfo(ffCharactersPath);
        }

        /// <summary>
        /// Gets the characters from logs.
        /// </summary>
        /// <param name='ffCharactersPath'>
        /// Ff characters path.
        /// </param>
        void GetCharacterInfo(string ffCharactersPath)
        {
            //Find Character Info
            if (Directory.Exists(ffCharactersPath))
            {
                string[] directories = Directory.GetDirectories(ffCharactersPath);
                for (int i = 0; i < directories.Length; ++i)
                {
                    if (directories[i].Contains(characterFolderSignerature) == false)
                    {
                        continue;
                    }
                    string logPath = directories[i] + "\\log";

                    if (Directory.Exists(logPath))
                    {
                        string[] files = Directory.GetFiles(logPath);

                        string characterName = null;
                        string serverName = null;

                        //Go through all files
                        for (int s = 0; s < files.Length; ++s)
                        {
                            //Look for a character name
                            if (characterName == null)
                            {
                                characterName = GetCharacterName(files[s]);
                            }
                            //Look for a server name
                            if (serverName == null)
                            {
                                serverName = GetServerName(files[s]);
                            }
                        }
                        if (characterName == null)
                        {
                            characterName = "Unknown Character";
                        }
                        if (serverName == null)
                        {
                            serverName = "Unknown Server";
                        }

                        //Save it.
                        characterLogs[characterName + " (" + serverName + ")"] = logPath;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the name of the character.
        /// </summary>
        /// <returns>
        /// The character name.
        /// </returns>
        /// <param name='filePath'>
        /// File path.
        /// </param>
        string GetCharacterName(string filePath)
        {
            string data = File.ReadAllText(filePath);
            List<string> logLines = new List<string>(data.Split(lineEndings, StringSplitOptions.RemoveEmptyEntries));

            for (int i = 0; i < logLines.Count; ++i)
            {
                //Split out extra lines.
                Match match = timeStampExpression.Match(logLines[i]);
                if (match.Success)
                {
                    int index = match.Index + 13;
                    logLines.Insert(i + 1, logLines[i].Substring(index, logLines[i].Length - index));
                    logLines[i] = logLines[i].Substring(0, index - 1);
                }

                //Tell sig
                if (logLines[i].StartsWith(tellSignerature))
                {
                    //Found a name.
                    string name = string.Concat(logLines[i].Skip(tellSignerature.Length).TakeWhile(letter => letter != '\u0003'));
                    return name.Trim();
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the name of the server.
        /// </summary>
        /// <returns>
        /// The server name.
        /// </returns>
        /// <param name='filePath'>
        /// File path.
        /// </param>
        string GetServerName(string filePath)
        {
            string data = File.ReadAllText(filePath);
            List<string> logLines = new List<string>(data.Split(lineEndings, StringSplitOptions.RemoveEmptyEntries));

            for (int i = 0; i < logLines.Count; ++i)
            {
                //Split out extra lines.
                Match match = timeStampExpression.Match(logLines[i]);
                if (match.Success)
                {
                    int index = match.Index + 13;
                    logLines.Insert(i + 1, logLines[i].Substring(index, logLines[i].Length - index));
                    logLines[i] = logLines[i].Substring(0, index - 1);
                }

                //Server Sig
                if (logLines[i].StartsWith(serverSignerature))
                {
                    //Found a name.
                    string name = string.Concat(logLines[i].Skip(serverSignerature.Length).TakeWhile(letter => letter != '!'));
                    return name.Trim();
                }
            }
            return null;
        }

        /// <summary>
        /// Clears the counters.
        /// </summary>
        /// <param name='dictionary'>
        /// Dictionary to clear.
        /// </param>
        void ClearCounters(Dictionary<ActionCounters, int> dictionary)
        {
            foreach (ActionCounters counter in Enum.GetValues(typeof(ActionCounters)))
            {
                dictionary[counter] = 0;
            }
        }

        /// <summary>
        /// Gets the node text recursively.
        /// </summary>
        /// <returns>
        /// The recursive node text.
        /// </returns>
        /// <param name='Nodes'>
        /// Nodes.
        /// </param>
        /// <param name='tabCount'>
        /// Tab count.
        /// increases by 1 every recursion.
        /// </param>
        string GetRecursiveNodeText(TreeNodeCollection Nodes, int tabCount)
        {
            string text = "";
            string tabs = "";
            for (int i = 0; i < tabCount; ++i)
            {
                tabs += "\t";
            }
            foreach (TreeNode node in Nodes)
            {
                text += tabs + node.Text + Environment.NewLine;
                text += GetRecursiveNodeText(node.Nodes, tabCount + 1);
            }
            return text;
        }

        /// <summary>
        /// Node sorter.
        /// </summary>
        class NodeSorter : IComparer
        {
            // compare between two tree nodes
            public int Compare(object thisObj, object otherObj)
            {
                TreeNode thisNode = thisObj as TreeNode;
                TreeNode otherNode = otherObj as TreeNode;

                //If no tag, sort alphabetical
                if (thisNode.Tag == null && otherNode.Tag == null)
                {
                    return thisNode.Text.CompareTo(otherNode.Text);
                }

                //These tags are always dates, so sort by date
                DateTime thisTime = (DateTime)thisNode.Tag;
                DateTime otherTime = (DateTime)otherNode.Tag;

                return otherTime.CompareTo(thisTime);
            }
        }

        /// <summary>
        /// Converts FF14 timestamp to datetime.
        /// </summary>
        /// <returns>
        /// The to date time.
        /// </returns>
        /// <param name='timeStamp'>
        /// Time stamp.
        /// </param>
        DateTime ConvertToDateTime(Int64 timeStamp)
        {
            //FF14 uses standard UNIX time with 1 second being 2^16 instead of 1
            double time = timeStamp / 65536.0;
            return epochTime.AddSeconds(time);
        }

        /// <summary>
        /// Gets the success rate string.
        /// </summary>
        /// <returns>
        /// The counter success rate text.
        /// </returns>
        /// <param name='counters'>
        /// Counters.
        /// </param>
        /// <param name='success'>
        /// Success.
        /// </param>
        /// <param name='tries'>
        /// Tries.
        /// </param>
        string GetCounterSuccessRateText(Dictionary<ActionCounters, int> counters, ActionCounters success, ActionCounters tries)
        {
            //Will return "(success)/(base)   ( {(success)/(base) %} )" unelss zero then will do "0/0"
            return ((counters[tries] > 0) ? string.Format("{0}/{1}   ( {2:P} )", counters[success], counters[tries], counters[success] / (float)counters[tries]) : "0/0");
        }

        /// <summary>
        /// Adds the item type node.
        /// </summary>
        /// <param name='lineTimeStamp'>
        /// Line time stamp.
        /// </param>
        /// <param name='displayText'>
        /// Display text.
        /// </param>
        /// <param name='itemType'>
        /// Item type.
        /// </param>
        void AddItemNode(Int64 lineTimeStamp, string displayText, string itemType)
        {
            if (itemType.StartsWith("a "))
            {
                itemType = itemType.Substring(2);
            }
            else if (itemType.StartsWith("an "))
            {
                itemType = itemType.Substring(3);
            }

            DateTime time = ConvertToDateTime(lineTimeStamp);
            string dateString = time.ToShortDateString();


            //Get a base node if one exists, otherwise make one.
            TreeNode dateNode = null;
            if (dateNodes.ContainsKey(dateString))
            {
                dateNode = dateNodes[dateString];
            }
            else
            {
                dateTreeView.Invoke((MethodInvoker)delegate()
                {
                    dateNode = new TreeNode(dateString);
                    dateNode.Tag = DateTime.Parse(dateString);
                    dateTreeView.Nodes.Add(dateNode);
                    dateNodes[dateString] = dateNode;
                });
            }

            //Get a base node if one exists, otherwise make one.
            TreeNode typeNode = null;
            if (itemTypeNodes.ContainsKey(itemType))
            {
                typeNode = itemTypeNodes[itemType];
            }
            else
            {
                itemTypeTreeView.Invoke((MethodInvoker)delegate()
                {
                    typeNode = itemTypeTreeView.Nodes.Add(itemType);
                    itemTypeNodes[itemType] = typeNode;
                });
            }

            //Create an item node and tag it with its line time.
            TreeNode itemNode = new TreeNode(displayText + " " + time.ToString("G"));
            itemNode.Tag = time;
            typeNode.Nodes.Add(itemNode);
            

            //Add more nodes for item craft data.
            foreach (Action craftAction in craftActions)
            {
                if (string.IsNullOrEmpty(craftAction.actionName))
                {
                    continue;
                }
                if (craftAction is FailableAction)
                {
                    FailableAction failAction = craftAction as FailableAction;
                    if (currentCraftCounters[failAction.usesCounter] > 0)
                    {
                        itemNode.Nodes.Add(failAction.actionName + ": " + GetCounterSuccessRateText(currentCraftCounters, failAction.successCounter, failAction.usesCounter));
                    }
                }
                else
                {
                    itemNode.Nodes.Add(craftAction.actionName + ": " + currentCraftCounters[craftAction.usesCounter]);
                }
            }

            dateTreeView.Invoke((MethodInvoker)delegate()
            {
                dateNode.Nodes.Add(itemNode.Clone() as TreeNode);
            });
            
        }

        /// <summary>
        /// Increases the failable couter.
        /// </summary>
        /// <param name='logLine'>
        /// Log line.
        /// </param>
        /// <param name='action'>
        /// Action.
        /// </param>
        void IncreaseFailableCouter(string logLine, Action action)
        {
            FailableAction failAction = action as FailableAction;

            bool success = logLine.Contains("Success");
            int steadyHandOffset = (1 + steadyHand) * 2;

            ++currentCraftCounters[failAction.usesCounter];
            ++currentCraftCounters[failAction.usesCounter + steadyHandOffset];
            if (success)
            {
                ++currentCraftCounters[failAction.successCounter];
                ++currentCraftCounters[failAction.successCounter + steadyHandOffset];
            }
        }

        /// <summary>
        /// Called when Steady Hand I/II is performed.
        /// </summary>
        /// <param name='logLine'>
        /// Log line.
        /// </param>
        /// <param name='action'>
        /// Action.
        /// </param>
        void GainSteadyHand(string logLine, Action action)
        {
            if (action.usesCounter == ActionCounters.SH1_Count)
            {
                steadyHand = 1;
            }
            if (action.usesCounter == ActionCounters.SH2_Count)
            {
                steadyHand = 2;
            }
        }

        /// <summary>
        /// Called when Steady Hand I/II is lost.
        /// </summary>
        /// <param name='logLine'>
        /// Log line.
        /// </param>
        /// <param name='action'>
        /// Action.
        /// </param>
        void LoseSteadyHand(string logLine, Action action)
        {
            steadyHand = 0;
        }

        /// <summary>
        /// Called when a crafting step is performed.
        /// </summary>
        /// <param name='logLine'>
        /// Log line.
        /// </param>
        /// <param name='action'>
        /// Action.
        /// </param>
        void CraftingStepCompleted(string logLine, Action action)
        {
            currentCraftCounters[ActionCounters.Steps_Completed]++;
        }

        /// <summary>
        /// Called when Tricks of the Trade is performed.
        /// </summary>
        /// <param name='logLine'>
        /// Log line.
        /// </param>
        /// <param name='action'>
        /// Action.
        /// </param>
        void UsedTricks(string logLine, Action action)
        {
            currentCraftCounters[ActionCounters.Tricks_Used]++;
        }

        /// <summary>
        /// Parses the log file.
        /// </summary>
        /// <param name='filePath'>
        /// File path.
        /// </param>
        void ParseLogFile(string filePath)
        {
            //Get the data from the file
            string data = File.ReadAllText(filePath);

            steadyHand = 0;

            //Time info
            Int64 craftStartTime = 0;
            Int64 lineTimeStamp = 0;

            //the name of the item we're currently crafting.
            string currentlyCrafting = "";

            //Split up the log lines.
            List<string> logLines = new List<string>(data.Split(lineEndings, StringSplitOptions.RemoveEmptyEntries));

            for (int i = 0; i < logLines.Count; ++i)
            {
                // Tries to find any extra end-lines that weren't found by the split.
                Match match = timeStampExpression.Match(logLines[i]);
                if (match.Success)
                {
                    int index = match.Index + 13;
                    logLines.Insert(i + 1, logLines[i].Substring(index, logLines[i].Length - index));
                    logLines[i] = logLines[i].Substring(0, index - 1);
                }

                // Strip out the DateTime tag on each line.
                if (logLines[i].Length > 12)
                {
                    string hexString = logLines[i].Substring(logLines[i].Length - 12, 12);
                    if (Regex.IsMatch(hexString, @"[A-F0-9]{12}"))
                    {
                        lineTimeStamp = Int64.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
                        logLines[i] = logLines[i].Substring(0, logLines[i].Length - 12);
                    }
                    else
                    {
                        continue;
                    }
                }

                if (useDateFilters)
                {
                    DateTime lineTime = ConvertToDateTime(lineTimeStamp);
                    if(lineTime > endDateFilter || lineTime < startDateFilter)
                    {
                        continue;
                    }
                }

                if (logLines[i].StartsWith("You begin synthesizing"))
                {
                    string displayText = readableTextExpression.Replace(logLines[i], "").Replace(" .", ".");
                    currentlyCrafting = displayText.Substring("You begin synthesizing ".Length, displayText.IndexOf(".") - "You begin synthesizing ".Length);

                    steadyHand = 0;
                    craftStartTime = lineTimeStamp;
                    ClearCounters(currentCraftCounters);
                }

                //Make sure we're traversing time correctly. Sometimes logs can get corrupted and will have sections out of place.
                //If this happens, we'll just throw this craft out, for accuracy purposes. We don't want to use potentially flawed data.
                if (lineTimeStamp < craftStartTime)
                {
                    craftStartTime = 0;
                    continue;
                }


                else if (craftStartTime > 0)
                {
                    for (int actionIndex = 0; actionIndex < craftActions.Length; ++actionIndex)
                    {
                        if (logLines[i].StartsWith(craftActions[actionIndex].logSignerature))
                        {
                            craftActions[actionIndex].actionFunction(logLines[i], craftActions[actionIndex]);
                        }
                    }

                    if (logLines[i].StartsWith("You synthesize "))
                    {
                        string displayText = currentlyCrafting;
                        bool HQ = logLines[i].Contains("");
                        if (HQ)
                        {
                            displayText = "(HQ) " + displayText;
                            totalCounters[ActionCounters.HQ_Crafts]++;
                        }
                        else
                        {
                            totalCounters[ActionCounters.NQ_Crafts]++;
                        }
                        AddItemNode(lineTimeStamp, displayText, currentlyCrafting);

                        foreach (KeyValuePair<ActionCounters, int> kvp in currentCraftCounters)
                        {
                            totalCounters[kvp.Key] += kvp.Value;
                        }

                        ClearCounters(currentCraftCounters);
                        craftStartTime = 0;
                    }
                    else if (logLines[i].StartsWith("Your synthesis fails"))
                    {
                        string displayText = "==FAILED== " + currentlyCrafting;
                        AddItemNode(lineTimeStamp, displayText, currentlyCrafting);

                        foreach (KeyValuePair<ActionCounters, int> kvp in currentCraftCounters)
                        {
                            totalCounters[kvp.Key] += kvp.Value;
                        }

                        totalCounters[ActionCounters.Failed_Crafts]++;

                        ClearCounters(currentCraftCounters);
                        craftStartTime = 0;
                    }
                    else if (logLines[i].StartsWith("You cancel the synth"))
                    {
                        string displayText = "==CANCELLED== " + currentlyCrafting;
                        AddItemNode(lineTimeStamp, displayText, currentlyCrafting);

                        foreach (KeyValuePair<ActionCounters, int> kvp in currentCraftCounters)
                        {
                            totalCounters[kvp.Key] += kvp.Value;
                        }

                        ClearCounters(currentCraftCounters);
                        craftStartTime = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Parses all of the logs from a directory.
        /// </summary>
        /// <returns>
        /// A text dump that is generated from the logs.
        /// </returns>
        /// <param name='directoryPath'>
        /// Directory path.
        /// </param>
        /// <param name="startDate">date to start parsing from</param>
        /// <param name="endDate">date to end parsing from</param>
        public string ParseLogsFromDirectory(string directoryPath, bool shouldUseDateFilters, DateTime startDate, DateTime endDate, ProgressBar parseProgress)
        {
            useDateFilters = shouldUseDateFilters;
            startDateFilter = startDate;
            endDateFilter = endDate;

            if (Directory.Exists(directoryPath))
            {
                ClearCounters(totalCounters);
                ClearCounters(currentCraftCounters);

                dateTreeView.Nodes.Clear();
                itemTypeTreeView.Nodes.Clear();

                dateTreeView.TreeViewNodeSorter = new NodeSorter();
                itemTypeTreeView.TreeViewNodeSorter = new NodeSorter();

                dateNodes.Clear();
                itemTypeNodes.Clear();

                string[] files = Directory.GetFiles(directoryPath);

                for (int i = 0; i < files.Length; ++i)
                {
                    parseProgress.Invoke(((MethodInvoker)delegate() { parseProgress.Value = (int)(100 * ((float)i / files.Length)); }));
                    ParseLogFile(files[i]);
                }

                string dumpText = "";

                //Add more nodes for item craft data.
                foreach (Action craftAction in craftActions)
                {
                    if (string.IsNullOrEmpty(craftAction.actionName))
                    {
                        continue;
                    }
                    if (craftAction is FailableAction)
                    {
                        FailableAction failAction = craftAction as FailableAction;
                        dumpText += failAction.actionName + ": " + GetCounterSuccessRateText(totalCounters, failAction.successCounter, failAction.usesCounter) + Environment.NewLine;
                        dumpText += failAction.actionName + " + SH0: " + GetCounterSuccessRateText(totalCounters, failAction.successCounter + 2, failAction.usesCounter + 2) + Environment.NewLine;
                        dumpText += failAction.actionName + " + SH1: " + GetCounterSuccessRateText(totalCounters, failAction.successCounter + 4, failAction.usesCounter + 4) + Environment.NewLine;
                        dumpText += failAction.actionName + " + SH2: " + GetCounterSuccessRateText(totalCounters, failAction.successCounter + 6, failAction.usesCounter + 6) + Environment.NewLine;

                    }
                    else
                    {
                        dumpText += craftAction.actionName + ": " + totalCounters[craftAction.usesCounter] + Environment.NewLine;
                    }
                }

                dumpText += "NQ Items: " + totalCounters[ActionCounters.NQ_Crafts] + Environment.NewLine;
                dumpText += "HQ Items: " + totalCounters[ActionCounters.HQ_Crafts] + Environment.NewLine;
                dumpText += "Failed Items: " + totalCounters[ActionCounters.Failed_Crafts] + Environment.NewLine;

                dumpText += Environment.NewLine + "Collaberation Data: " + Environment.NewLine;

                foreach (int value in totalCounters.Values)
                {
                    dumpText += value + "\t";
                }

                dumpText += Environment.NewLine + Environment.NewLine + GetRecursiveNodeText(dateTreeView.Nodes, 0);

                return dumpText;
            }
            return null;
        }
    }
}