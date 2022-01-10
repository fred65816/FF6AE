using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace FFVIEditor
{
    public class FF6Arom
    {
        # region CLASS ATTRIBUTES

        // Result of the complementary check
        public bool complementaryTest;

        // A value to comparer with the complementaryCheck 
        public int complement { get; private set; }

        // A value to compare with the complement
        public int complementaryCheck { get; private set; }

        // The country name 
        public string country { get; private set; }

        // The country character of the ROM (U, E, J)
        public char countryChar { get; private set; }

        // The 32 bits CRC32 number.
        public Int32 crc32 { get; private set; }

        // The bytes array containing the file data.
        public byte[] fileDataArray { get; private set; }

        // The file name of the ROM. Ex: "Final Fantasy VI Advance (E).gba".
        public string fileName { get; private set; }

        // The valid full path of the ROM. Ex: "C:\GBA roms\Final Fantasy VI Advance (E).gba".
        public string filePath { get; private set; }

        // The file size of the ROM in Mega-octets
        public double fileSize { get; private set; }

        // The game code
        public string gameCode { get; private set; }

        // The version of the game
        public int gameVersion { get; private set; }

        // The name of the ROM
        public string romName { get; private set; }

        // The game maker byte
        public int makerByte { get; private set; }

        // The game maker code
        public string makerCode { get; private set; }

        // The game maker name
        public string makerName { get; private set; }

        // The starting offset of the save type
        public string saveOffset { get; private set; }

        // The save type of the ROM
        public string saveType { get; private set; }

        // The serial number of the ROM
        public string serial { get; private set; }

        public bool success { get; private set; }

        # endregion

        # region CONSTRUCTOR

        /// <summary>
        /// All the class attributes are set in the constructor,, by direct assignation or assignation methods called.
        /// </summary>
        /// <param name="filePath">The full path of the ROM. Ex: "C:\GBAroms\Final Fantasy Advance (e).gba"</param>
        public FF6Arom(string filePath)
        {            
            #region DIRECT ASSIGNATIONS

            // Set the filePath attribute.
            this.filePath = filePath;

            // complementary test is set to fail by default
            complementaryTest = false;

            #endregion

            #region CLASS ATTRIBUTE ASSIGNATION METHODS

            // Set the fileName attribute.
            fileName = SetFileName(filePath);

            // Set the crc32 attribute.
            CalculateCrc32();

            // Set the fileSize attribute and fill the fileDataArray attribute.
            ReadAllFile();

            // Set the gameVersion, romName, gameCode, coutryChar, country and serial attributes.
            GetSerial();

            // Set the makerCode, makerByte and makerName attributes.
            GetMaker();

            // Set the save method and save method offset
            GetSaveMethod();

            #endregion

            #region COMPLEMENTARY CHECK

            // Perform check to allow information display
            ComplementaryCheck();

            #endregion
        }

        # endregion

        # region PRIVATE METHODS

        /// <summary>
        /// This method simply separate the fileName from the full path.
        /// Ex: filePath = "C:\GBA roms\Final Fantasy VI Advance (E).gba" -> fileName = "Final Fantasy VI Advance (E).gba".
        /// </summary>
        /// <param name="filePath">Full path of the file. Ex: "C:\GBA roms\Final Fantasy VI Advance (E).gba".</param>
        /// <returns>the file name. Ex: "Final Fantasy VI Advance (E).gba".</returns>
        private string SetFileName(string filePath)
        {
            #region FILE NAME SEPARATION

            string fileName = Path.GetFileName(filePath);

            #endregion

            #region RETURN VALUE

            return fileName;

            #endregion
        }

        /// <summary>
        /// Create the CRC table and calculate the CRC32 value of the ROM and intiate the crc32 variable to the good value.
        /// </summary>
        private void CalculateCrc32()
        {
            #region LOCAL ATTRIBUTES

            // Create a new CRC32 table and object for calculation.
            CalculateCrc crc = new CalculateCrc();

            // Read the first 8192 bytes of the ROM for CRC32 calculation.
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192);

            #endregion

            #region CLASS ATTRIBUTE ASSIGNATION

            // Calculate the CRC32 number.
            crc32 = crc.GetCrc32(fileStream);

            #endregion

            #region DISPOSITION

            // empty and close the file stream.
            fileStream.Flush();
            fileStream.Close();

            // Nullify and release objects
            //RomUtils.releaseObject(fileStream);
            //RomUtils.releaseObject(crc);

            // Clean memory
            GC.Collect();

            #endregion
        }

        /// <summary>
        /// Read all the data from the ROM and store it in the fileDataArray attribute. Set the fileSize attribute.
        /// </summary>
        private void ReadAllFile()
        {

            #region LOCAL ATTRIBUTES

            Int32 fileLength = 0;

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            #endregion

            #region CLASS ATTRIBUTE ASSIGNATION

            // Temporary try/catch. Must fix this eventually.
            try
            {
                // Convert Int64 file length to int.
                fileLength = (Int32)fileInfo.Length;

                // Calculate the file size in mega octects
                fileSize = (fileLength / 1024) / 1024;
            }
            catch (Exception)
            {
                Console.WriteLine("Impossible to convert file size from long to int. File too big.");
            }

            #endregion

            #region BINARY READING

            // Create binary reader to read all the file.
            BinaryReader binaryReader = new BinaryReader(File.OpenRead(filePath));

            // Read all file and store the data in fileDataArray.
            fileDataArray = binaryReader.ReadBytes(fileLength);

            #endregion

            #region DISPOSITION

            // Empty and close the reader
            binaryReader.BaseStream.Flush();
            binaryReader.Close();

            // Release objects
            //RomUtils.releaseObject(fileInfo);
            //RomUtils.releaseObject(binaryReader);

            // Clean memory
            GC.Collect();

            #endregion
        }

        /// <summary>
        /// This method set the gameVersion, romName, gameCode, coutryChar, country and serial attributes.
        /// </summary>
        private void GetSerial()
        {
            #region LOCAL ATTRIBUTES

            string tempCountry = null;
            string tempSerial = null;
            bool serialFound = false;
            
            #endregion

            #region VARIABLE INITIALIZATION

            romName = "";
            gameCode = "";

            #endregion

            #region CLASS ATTRIBUTE ASSIGNATION

            //Set the game version.
            gameVersion = fileDataArray[188];

            //Build the name of the ROM. Bytes 160 to 171 of the header.
            for (int i = 160; i <= 171; i++)
            {
                romName += Convert.ToChar(fileDataArray[i]);
            }

            //Build game code. Bytes 172 to 175 of the header.
            for (int i = 172; i <= 175; i++)
            {
                gameCode += Convert.ToChar(fileDataArray[i]);
            }

            // The country letter for the version of the game (E, J, P, X, I, D, G, F or Y).
            char countrychar = gameCode.Substring(3, 1)[0];

            // Determine the serial number and country of the game.
            serialFound = RomUtils.DetermineSerial(gameCode, countrychar, out tempCountry, out tempSerial);

            // Assign the output values to class variables. The null check is kinda useless...
            if (tempCountry != null && tempSerial != null)
            {
                country = tempCountry; serial = tempSerial;
            }

            #endregion

            #region RETURN VALUE

            //return serialFound;

            #endregion
        }

        /// <summary>
        /// This method set the makerCode, makerByte and makerName attributes.
        /// </summary>
        private void GetMaker()
        {
            #region CLASS ATTRIBUTE ASSIGNATION

            // Two characters maker code. Bytes 176 and 177 of the header.
            makerCode = Convert.ToChar(fileDataArray[176]).ToString() + Convert.ToChar(fileDataArray[177]).ToString();

            // Convert the maker code to hex. Not sure about this one.
            makerByte = Convert.ToInt32(makerCode, 16);

            // Determine the license name. Ex: "51 : Squaresoft".
            makerName = RomUtils.DetermineLicense(makerByte);

            #endregion
        }

        /// <summary>
        /// This method set the saveMethod and saveOffset attributes.
        /// </summary>
        private void GetSaveMethod()
        {
            #region CLASS ATTRIBUTE ASSIGNATION

            // Get the save type of the ROM (EPPROM, SRAM or FLASH).
            saveType = RomUtils.DetermineSaveType(fileDataArray);

            // Get the starting offset of the save method.
            saveOffset = saveType.Substring(saveType.LastIndexOf("-") + 1);

            // Islate the name of the save method.
            saveType = saveType.Substring(0, saveType.IndexOf("--"));

            #endregion
        }

        /// <summary>
        /// This method perform a complementary check on the ROM. Two values are compared.
        /// If they match, the test is passed.
        /// </summary>
        private void ComplementaryCheck()
        {
            #region PERFORM TEST

            // Assign byte 189
            complement = fileDataArray[189];

            // Perform check on the header
            complementaryCheck = RomUtils.ComplementaryCheck(fileDataArray);

            // If both values are equal, the test is passed.
            if (complement == complementaryCheck)
            {
                complementaryTest = true;
            }

            #endregion
        }

        private string checkStringValidity(string stringToCheck)
        {
            if (stringToCheck.Equals("") || stringToCheck == null)
            {
                stringToCheck = "Error!";
            }

            return stringToCheck;
        }

        private void displayResults()
        {
            romName = checkStringValidity(romName);
            country = checkStringValidity(country);
            makerName = checkStringValidity(makerName);
            saveType = checkStringValidity(saveType);

        }

        private void showResults()
        {
            // Display result of test
            Console.WriteLine("Complement: " + complement.ToString());
            Console.WriteLine("ComplementaryCheck: " + complementaryCheck.ToString());
            Console.WriteLine("----------------------");

            // Display all the info if test has passed
            if (complementaryTest)
            {
                Console.WriteLine("FileName: " + fileName);
                Console.WriteLine("RomName: " + romName);
                Console.WriteLine("GameCode: " + gameCode);
                Console.WriteLine("GameVersion: " + gameVersion.ToString());
                Console.WriteLine("FileSize: " + fileSize.ToString());
                Console.WriteLine("CountryChar: " + countryChar);
                Console.WriteLine("Country: " + country);
                Console.WriteLine("MakerByte: " + makerByte.ToString());
                Console.WriteLine("MakerCode: " + makerCode);
                Console.WriteLine("MakerName: " + makerName);
                Console.WriteLine("SaveType: " + saveType);
                Console.WriteLine("SaveOffset: " + saveOffset);
                Console.WriteLine("Serial: " + serial);
                Console.WriteLine("CRC32: " + crc32.ToString());

                // Press a key to exit the Console
                Console.ReadLine();
            }
        }

        # endregion
    }
}
