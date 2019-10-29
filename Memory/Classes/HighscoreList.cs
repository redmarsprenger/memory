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
        /// <summary>
        /// singleton object of the class
        /// </summary>
        private static HighscoreList highscoreList;
        
        /// <summary>
        /// list of the highscores that were loaded
        /// </summary>
        private List<Highscore> Highscores;
        
        /// <summary>
        /// binary formatter used to save and load the data
        /// </summary>
        private BinaryFormatter formatter;

        /// <summary>
        /// name of the file that is created (its located somewhere in the bin)
        /// </summary>
        private const string DATA_FILENAME = "highscorelist.dat";

        /// <summary>
        /// creates the singleton object, so that there can't exist multiple of this object
        /// </summary>
        /// <returns>the singleton object</returns>
        public static HighscoreList Instance()
        {
            if (highscoreList == null)
            {
                highscoreList = new HighscoreList();
            }
            return highscoreList;
        }

        /// <summary>
        /// constructor initializes the properties
        /// </summary>
        private HighscoreList()
        {
            this.Highscores = new List<Highscore>();
            this.formatter = new BinaryFormatter();
        }

        /// <summary>
        /// takes the data that is added and saves it in the file
        /// </summary>
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

        /// <summary>
        /// reads the data out of the file and puts it in the object
        /// </summary>
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

        /// <summary>
        /// fetches the list of highscores
        /// </summary>
        /// <returns>the list of highscores</returns>
        public List<Highscore> GetList()
        {
            return this.Highscores;
        }

    }
}
