using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Classes
{
    public class HighscoreList
    {
        //https://stackoverflow.com/questions/6115721/how-to-save-restore-serializable-object-to-from-file

        private static HighscoreList highscoreList;

        private List<Highscore> Highscores;
        private BinaryFormatter formatter;

        private const string DATA_FILENAME = "highscorelist.dat";

        public static HighscoreList Instance()
        {
            if (highscoreList == null)
            {
                highscoreList = new HighscoreList();
            }
            return highscoreList;
        }

        private HighscoreList()
        {
            this.Highscores = new List<Highscore>();
            this.formatter = new BinaryFormatter();
        }

        public void Save()
        {
            try
            {
                FileStream writerFileStream = new FileStream(DATA_FILENAME, FileMode.Append, FileAccess.Write);

                this.formatter.Serialize(writerFileStream, this.Highscores);

                writerFileStream.Close();
            }
            catch (Exception)
            {
                //popup or something else that gets thrown when the save doesn't happen
            }
        }

        public void Load()
        {
            if (File.Exists(DATA_FILENAME))
            {
                try
                {
                    FileStream readerFileStream = new FileStream(DATA_FILENAME, FileMode.Open, FileAccess.Read);

                    this.Highscores = (List<Highscore>)this.formatter.Deserialize(readerFileStream);

                    readerFileStream.Close();
                }
                catch (Exception)
                {
                    //popup or something to show if the load wasn't successfull
                }
            }
        }

        /// <summary>
        /// To add a new highscore to the list in the instance
        /// </summary>
        /// <param name="highscore">A new highscore object</param>
        public void AddHighscore(Highscore highscore)
        {
            this.Highscores.Add(highscore);
        }

        public List<Highscore> GetList()
        {
            return this.Highscores;
        }

    }
}
