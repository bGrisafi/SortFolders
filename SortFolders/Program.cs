using System.Reflection.Metadata;

namespace SortFolders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Variables
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            string[] sortingFolders = new string[29];

            //generates the sortig folders
            short counter = 0;
            foreach (var letter in alphabet)
            {
                sortingFolders[counter] = letter.ToString();
                counter++;
            }
            sortingFolders[counter] = "_autres";
            sortingFolders[counter + 1] = "_fichiers";
            sortingFolders[counter + 2] = "_non trié";



            Console.WriteLine("===== Sort Folders =====");

            //Get folders
            Console.WriteLine("Veuillez indiquer le répertoire dont les dossiers sont à trier: ");
            string sourceFolderAddress = Console.ReadLine().Trim();
            Console.WriteLine();
            Console.WriteLine("Veuillez indiquer le répertoire de destination ou générer les dossiers de tri et transférer les dossiers triés: ");
            string targetFolderAddress = Console.ReadLine().Trim();
            Console.WriteLine();


            //Check and generates sorting folders

            //Get source folders and remove address
            string[] sourceFoldersUnsorted = Directory.GetDirectories(sourceFolderAddress);
            string[] sourceFolders = new string[sourceFoldersUnsorted.Length];
            string sourcePath = "";


            for (int i = 0; i < sourceFoldersUnsorted.Length; i++)
            {
                string[] subDirectoryExploded = sourceFoldersUnsorted[i].Split('\\');
                sourceFolders[i] = subDirectoryExploded[subDirectoryExploded.Length - 1];
                if(i == 0)
                {
                    for (int j = 0; j < subDirectoryExploded.Length - 1; j++)
                    {
                        sourcePath += subDirectoryExploded[j] + "\\";
                    }
                }
            }



            //removes the sorting folders from the source folder list if they are present
            for (int i = 0; i < sourceFolders.Length; i++)
            {
                for (int j = 0; j < sortingFolders.Length; j++)
                {
                    if (sourceFolders[i] != null)
                    {
                        if (sortingFolders[j].Equals(sourceFolders[i].ToUpper()))
                        {
                            sourceFolders[i] = null;
                        }
                    }

                }
            }

            //generates the sorting folders at the target folder

            foreach (var folder in sortingFolders)
            {
                string newDirectory = targetFolderAddress + "\\" + folder;
                Directory.CreateDirectory(newDirectory);
            }


            //sort folders
            foreach (var folder in sourceFolders)
            {
                if (!string.IsNullOrEmpty(folder))
                {
                    var isSorted = false;
                    var i = 0;
                    while (!isSorted && i < 26)
                    {
                        if(sortingFolders[i].Equals(folder.Substring(0,1).ToUpper()))
                        {
                            string origin = sourcePath + folder;
                            string target = targetFolderAddress + "\\" + sortingFolders[i] + "\\" + folder;

                            if (!Directory.Exists(target))
                            {
                                Directory.Move(origin, target);

                            }
                            else
                            {
                                Console.WriteLine("Erreur ! le dossier " + folder + " existe déjà.");
                            }
                            isSorted = true;
                        }
                        i++;
                    }
                    if (!isSorted)
                    {
                        string origin = sourcePath + folder;
                        string target = targetFolderAddress + "\\" + sortingFolders[i] + "\\" + folder;

                        if (!Directory.Exists(target))
                        {
                            Directory.Move(origin, target);

                        }
                        else
                        {
                            Console.WriteLine("Erreur ! le dossier " + folder + " existe déjà.");
                        }
                        isSorted = true;
                    }
                }
            }
            Console.WriteLine("Fin du programme, appuyez sur n'importe quelle touche ...");
            Console.ReadKey(); 
        }
    }
}