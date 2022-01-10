
using System;
using System.Runtime.InteropServices;
namespace FFVIEditor
{
    class RomUtils
    {
        # region PUBLIC STATIC METHODS

        /// <summary>
        /// This method nullify and release an object from memory. Prevents memory leaks.
        /// </summary>
        /// <param name="obj">The object that has been used and serve no other purpose.</param>
        public static void releaseObject(object obj)
        {
            #region NULLIFY AND RELEASE

            Marshal.ReleaseComObject(obj);

            #endregion
        }

        /// <summary>
        /// Convert a base 10 number into a Hexadecimal number.
        /// </summary>
        /// <param name="tempDecimal"></param>
        /// <returns>The hex result of the conversion.</returns>
        public static string convertfrombase10(int tempDecimal)
        {
            #region RETURN VALUE

            return Convert.ToString(tempDecimal, 16).ToUpper();

            #endregion
        }

        /// <summary>
        /// This method convert a hex into a int32
        /// </summary>
        /// <param name="hexValue"></param>
        /// <returns>The int32 result of the conversion</returns>
        public static Int32 convertfrombase16(string hexValue)
        {
            #region RETURN VALUE

            return Convert.ToInt32(hexValue, 16);

            #endregion
        }

        /// <summary>
        /// This method convert a string to an array of bytes.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>The resulting array of bytes</returns>
        public static byte[] StringToBytes(string str)
        {
            #region LOCAL ATTRIBUTES

            // Length of the string
            int strLength = str.Length - 1;

            // The array of bytes to store the results of conversions. Length is the same as the string.
            byte[] resultArray = new byte[strLength];

            #endregion

            #region CONVERSION LOOP

            // Loop for every char of the string
            for (int i = 0; i < strLength; i++)
            {
                //Convert to byte the current char of the string
                resultArray[i] = Convert.ToByte(str[i]);
            }

            #endregion

            #region RETURN VALUE

            return resultArray;

            #endregion
        }

        /// <summary>
        /// Search for a child array into a main array.
        /// </summary>
        /// <param name="mainArray">The main array. This array could contain the child array.</param>
        /// <param name="childArray">The child array. Could be contained in the main array.</param>
        /// <returns>The return value is the position (index) of the child array in the main array.</returns>
        public static int SearchArrayInArray(byte[] mainArray, byte[] childArray)
        {
            #region LOCAL ATTRIBUTES

            // i and j are used for looping purpose
            int i = 0;
            int j;

            // boolean used for exiting the loop. Will be true when the position of the child array is found.
            bool boolFound = false;

            // Length of the child array
            int childArrayLength = childArray.Length - 1;

            // Difference of length between the main and child arrays
            int arraysLengthDiff = mainArray.Length - childArray.Length;

            // The position of the child array into the main array
            int returnPosition = 0;

            #endregion

            #region SEARCH ALGORITHM

            // Loop main array as long as child array is not found
            while (i <= arraysLengthDiff && !boolFound)
            {
                // Look for a first possible matching character
                if (mainArray[i] == childArray[0])
                {
                    j = 0;
                    boolFound = true;

                    // Loop the child array as long as characters match
                    while (j <= childArrayLength && boolFound)
                    {
                        // Compare subsequent characters of both arrays
                        if (mainArray[i + j] != childArray[j])
                        {
                            boolFound = false;
                        }
                        j++;
                    }
                    if (boolFound)
                    {
                        returnPosition = i;
                    }
                }
                i++;
            }

            #endregion

            #region DISPOSITION

            //// Release objects
            //releaseObject(mainArray);
            //releaseObject(childArray);

            //// Clean memory
            //GC.Collect();

            #endregion

            #region RETURN VALUE

            // return 0 if the child arrays was not found in the main array
            return returnPosition;

            #endregion
        }


        /// <summary>
        /// This function construct the serial number with the game code value passed as the first parameter.
        /// </summary>
        /// <param name="gameCode">The game code.</param>
        /// <param name="country">The country of the version of the game (USA, JAP, EUR, EUU, ITA, DEU or FRA). This is an output parameter.</param>
        /// <param name="serial">The serial number of the game. This is an output parameter.</param>
        /// <returns></returns>
        public static bool DetermineSerial(string gameCode, char countryChar, out string country, out string serial)
        {
            # region LOCAL ATTRIBUTES

            // By default we assume it is not an unknown game
            bool serialFound = true; 

            # endregion

            # region SERIAL CONSTRUCTION

            switch (countryChar)
            {
                case 'E':
                    serial = "AGB-" + gameCode + "-USA"; country = "USA"; break;
                case 'J':
                    serial = "AGB-" + gameCode + "-JPN"; country = "JAP"; break;
                case 'P':
                    serial = "AGB-" + gameCode + "-EUR"; country = "EUR"; break;
                case 'X':
                    serial = "AGB-" + gameCode + "-EUU"; country = "EUU"; break;
                case 'I':
                    serial = "AGB-" + gameCode + "-ITA"; country = "ITA"; break;
                case 'D':
                    serial = "AGB-" + gameCode + "-NOE"; country = "DEU"; break;
                case 'G':
                    serial = "AGB-" + gameCode + "-NOE"; country = "FRA"; break;
                case 'F':
                    serial = "AGB-" + gameCode + "-FRA"; country = "FRA"; break;
                case 'Y':
                    serial = "AGB-" + gameCode + "-EUU"; country = "EUU"; break;
                default:
                    serial = "AGB-" + gameCode + "-???"; country = "???"; serialFound = false; break;
            }

            # endregion

            # region RETURN VALUE

            // The return value will be false only if country character was not in the choices availables
            return serialFound;

            # endregion
        }

        /// <summary>
        /// This method construct the license string with the game maker byte followed by the game maker name
        /// </summary>
        /// <param name="makerByte">The byte of the game maker (1 to 254, 255 being used for unknown maker)</param>
        /// <returns>The license string. Ex: "1 : Nintendo" or "51 : Squaresoft"</returns>
        public static string DetermineLicense(int makerByte)
        {
            #region LOCAL ATTRIBUTE

            string strMakerByte = makerByte.ToString();

            #endregion

            #region LICENSE BUILDING

            //determine the license depending on the byte read
            switch (makerByte)
            {
                case 1:
                    return strMakerByte + " : Nintendo";
                case 3:
                    return strMakerByte + " : Imagineer-Zoom";
                case 5:
                    return strMakerByte + " : Zamuse";
                case 6:
                    return strMakerByte + " : Falcom";
                case 8:
                    return strMakerByte + " : Capcom";
                case 9:
                    return strMakerByte + " : HOT-B";
                case 10:
                    return strMakerByte + " : Jaleco";
                case 11:
                    return strMakerByte + " : Coconuts";
                case 12:
                    return strMakerByte + " : Rage Software";
                case 14:
                    return strMakerByte + " : Technos";
                case 15:
                    return strMakerByte + " : Mebio Software";
                case 18:
                    return strMakerByte + " : Gremlin Graphics";
                case 19:
                    return strMakerByte + " : Electronic Arts";
                case 21:
                    return strMakerByte + " : COBRA Team";
                case 22:
                    return strMakerByte + " : Human/Field";
                case 23:
                    return strMakerByte + " : KOEI";
                case 24:
                    return strMakerByte + " : Hudson Soft";
                case 26:
                    return strMakerByte + " : Yanoman";
                case 28:
                    return strMakerByte + " : Tecmo";
                case 30:
                    return strMakerByte + " : Open System";
                case 31:
                    return strMakerByte + " : Virgin Games";
                case 32:
                    return strMakerByte + " : KSS";
                case 33:
                    return strMakerByte + " : Sunsoft";
                case 34:
                    return strMakerByte + " : POW";
                case 35:
                    return strMakerByte + " : Micro World";
                case 38:
                    return strMakerByte + " : Enix";
                case 39:
                    return strMakerByte + " : Loriciel/Electro Brain";
                case 40:
                    return strMakerByte + " : Kemco";
                case 41:
                    return strMakerByte + " : Seta Co.,Ltd.";
                case 45:
                    return strMakerByte + " : Visit Co.,Ltd.";
                case 49:
                    return strMakerByte + " : Carrozzeria";
                case 50:
                    return strMakerByte + " : Dynamic";
                case 51:
                    return strMakerByte + " : Squaresoft";
                case 52:
                    return strMakerByte + " : Magifact";
                case 53:
                    return strMakerByte + " : Hect";
                case 60:
                    return strMakerByte + " : Empire Software";
                case 61:
                    return strMakerByte + " : Loriciel";
                case 64:
                    return strMakerByte + " : Seika Corp.";
                case 65:
                    return strMakerByte + " : UBI Soft";
                case 70:
                    return strMakerByte + " : System 3";
                case 71:
                    return strMakerByte + " : Spectrum Holobyte";
                case 73:
                    return strMakerByte + " : Irem";
                case 75:
                    return strMakerByte + " : Raya Systems/Sculptured Software";
                case 76:
                    return strMakerByte + " : Renovation Products";
                case 77:
                    return strMakerByte + " : Malibu Games/Black Pearl";
                case 79:
                    return strMakerByte + " : U.S. Gold";
                case 80:
                    return strMakerByte + " : Absolute Entertainment";
                case 81:
                    return strMakerByte + " : Acclaim";
                case 82:
                    return strMakerByte + " : Activision";
                case 83:
                    return strMakerByte + " : American Sammy";
                case 84:
                    return strMakerByte + " : GameTek";
                case 85:
                    return strMakerByte + " : Hi Tech Expressions";
                case 86:
                    return strMakerByte + " : LJN Toys";
                case 90:
                    return strMakerByte + " : Mindscape";
                case 93:
                    return strMakerByte + " : Tradewest";
                case 95:
                    return strMakerByte + " : American Softworks Corp.";
                case 96:
                    return strMakerByte + " : Titus";
                case 97:
                    return strMakerByte + " : Virgin Interactive Entertainment";
                case 98:
                    return strMakerByte + " : Maxis";
                case 103:
                    return strMakerByte + " : Ocean";
                case 105:
                    return strMakerByte + " : Electronic Arts";
                case 107:
                    return strMakerByte + " : Laser Beam";
                case 110:
                    return strMakerByte + " : Elite";
                case 111:
                    return strMakerByte + " : Electro Brain";
                case 112:
                    return strMakerByte + " : Infogrames";
                case 113:
                    return strMakerByte + " : Interplay";
                case 114:
                    return strMakerByte + " : LucasArts";
                case 115:
                    return strMakerByte + " : Parker Brothers";
                case 117:
                    return strMakerByte + " : STORM";
                case 120:
                    return strMakerByte + " : THQ Software";
                case 121:
                    return strMakerByte + " : Accolade Inc.";
                case 122:
                    return strMakerByte + " : Triffix Entertainment";
                case 124:
                    return strMakerByte + " : Microprose";
                case 127:
                    return strMakerByte + " : Kemco";
                case 128:
                    return strMakerByte + " : Misawa";
                case 129:
                    return strMakerByte + " : Teichio";
                case 130:
                    return strMakerByte + " : Namco Ltd.";
                case 131:
                    return strMakerByte + " : Lozc";
                case 132:
                    return strMakerByte + " : Koei";
                case 134:
                    return strMakerByte + " : Tokuma Shoten Intermedia";
                case 136:
                    return strMakerByte + " : DATAM-Polystar";
                case 139:
                    return strMakerByte + " : Bullet-Proof Software";
                case 140:
                    return strMakerByte + " : Vic Tokai";
                case 142:
                    return strMakerByte + " : Character Soft";
                case 143:
                    return strMakerByte + " : I''Max";
                case 144:
                    return strMakerByte + " : Takara";
                case 145:
                    return strMakerByte + " : CHUN Soft";
                case 146:
                    return strMakerByte + " : Video System Co., Ltd.";
                case 147:
                    return strMakerByte + " : BEC";
                case 149:
                    return strMakerByte + " : Varie";
                case 151:
                    return strMakerByte + " : Kaneco";
                case 153:
                    return strMakerByte + " : Pack in Video";
                case 154:
                    return strMakerByte + " : Nichibutsu";
                case 155:
                    return strMakerByte + " : TECMO";
                case 156:
                    return strMakerByte + " : Imagineer Co.";
                case 160:
                    return strMakerByte + " : Telenet";
                case 164:
                    return strMakerByte + " : Konami";
                case 165:
                    return strMakerByte + " : K.Amusement Leasing Co.";
                case 167:
                    return strMakerByte + " : Takara";
                case 169:
                    return strMakerByte + " : Technos Jap.";
                case 170:
                    return strMakerByte + " : JVC";
                case 172:
                    return strMakerByte + " : Toei Animation";
                case 173:
                    return strMakerByte + " : Toho";
                case 175:
                    return strMakerByte + " : Namco Ltd.";
                case 177:
                    return strMakerByte + " : ASCII Co. Activison";
                case 178:
                    return strMakerByte + " : BanDai America";
                case 180:
                    return strMakerByte + " : Enix";
                case 182:
                    return strMakerByte + " : Halken";
                case 186:
                    return strMakerByte + " : Culture Brain";
                case 187:
                    return strMakerByte + " : Sunsoft";
                case 188:
                    return strMakerByte + " : Toshiba EMI";
                case 189:
                    return strMakerByte + " : Sony Imagesoft";
                case 191:
                    return strMakerByte + " : Sammy";
                case 192:
                    return strMakerByte + " : Taito";
                case 194:
                    return strMakerByte + " : Kemco";
                case 195:
                    return strMakerByte + " : Square";
                case 196:
                    return strMakerByte + " : Tokuma Soft";
                case 197:
                    return strMakerByte + " : Data East";
                case 198:
                    return strMakerByte + " : Tonkin House";
                case 200:
                    return strMakerByte + " : KOEI";
                case 202:
                    return strMakerByte + " : Konami USA";
                case 203:
                    return strMakerByte + " : NTVIC";
                case 205:
                    return strMakerByte + " : Meldac";
                case 206:
                    return strMakerByte + " : Pony Canyon";
                case 207:
                    return strMakerByte + " : Sotsu Agency/Sunrise";
                case 208:
                    return strMakerByte + " : Disco/Taito";
                case 209:
                    return strMakerByte + " : Sofel";
                case 210:
                    return strMakerByte + " : Quest Corp.";
                case 211:
                    return strMakerByte + " : Sigma";
                case 214:
                    return strMakerByte + " : Naxat";
                case 216:
                    return strMakerByte + " : Capcom Co., Ltd.";
                case 217:
                    return strMakerByte + " : Banpresto";
                case 218:
                    return strMakerByte + " : Tomy";
                case 219:
                    return strMakerByte + " : Acclaim";
                case 221:
                    return strMakerByte + " : NCS";
                case 222:
                    return strMakerByte + " : Human Entertainment";
                case 223:
                    return strMakerByte + " : Altron";
                case 224:
                    return strMakerByte + " : Jaleco";
                case 226:
                    return strMakerByte + " : Yutaka";
                case 228:
                    return strMakerByte + " : T&ESoft";
                case 229:
                    return strMakerByte + " : EPOCH Co.,Ltd.";
                case 231:
                    return strMakerByte + " : Athena";
                case 232:
                    return strMakerByte + " : Asmik";
                case 233:
                    return strMakerByte + " : Natsume";
                case 234:
                    return strMakerByte + " : King Records";
                case 235:
                    return strMakerByte + " : Atlus";
                case 236:
                    return strMakerByte + " : Sony Music Entertainment";
                case 238:
                    return strMakerByte + " : IGS";
                case 241:
                    return strMakerByte + " : Motown Software";
                case 242:
                    return strMakerByte + " : Left Field Entertainment";
                case 243:
                    return strMakerByte + " : Beam Software";
                case 244:
                    return strMakerByte + " : Tec Magik";
                case 249:
                    return strMakerByte + " : Cybersoft";
                case 255:
                    return strMakerByte + " : Hudson Soft";
                default:
                    return strMakerByte + " : Unknow license";
            }

            #endregion
        }




        public static string DetermineSaveType(byte[] fileDataArray)
        {
            #region LOCAL ATTRIBUTES

            // Name of the save method.
            string saveType = "";

            // Temporary int used for incrementation purpose in the Flash save type.
            int tempInt = 0;

            // The result of the search for the different methods. It will return 0 if the method is not found.
            int searchResult;

            // We assume by default that the method is not found.
            bool saveMethodFound = false;

            #endregion

            #region EEPROM METHOD SEARCH

            // Search for EEPROM save method.
            searchResult = SearchArrayInArray(fileDataArray, StringToBytes("EEPROM_"));

            // If the search is successfull
            if (searchResult > 0)
            {
                saveMethodFound = true;

                for (int i = 0; i <= 10; i++)
                {
                    // Build the save method name (11 letters).
                    saveType += Convert.ToChar(fileDataArray[searchResult + i]);
                }

                // Add "----" and position of the word to the save type string.
                saveType += ("----" + searchResult.ToString());
            }

            #endregion

            #region SRAM METHOD SEARCH

            // If first search was not successfull
            if (!saveMethodFound)
            {
                // Search for SRAM save method.
                searchResult = SearchArrayInArray(fileDataArray, StringToBytes("SRAM_"));

                if (searchResult > 0)
                {
                    saveMethodFound = true;

                    for (int i = 0; i <= 9; i++)
                    {
                        // Build the save method name (10 letters).
                        saveType += Convert.ToChar(fileDataArray[searchResult + i]);
                    }
                    if (saveType.Substring(4, 1) != "_")
                    {
                        saveType = saveType.Substring(0, 4);
                    }

                    // Add "----" and position of the word to the save type string.
                    saveType += ("----" + searchResult.ToString());
                }
            }

            #endregion

            #region FLASH METHOD SEARCH

            // If first two searches were not successfull
            if (!saveMethodFound)
            {
                // Search for FLASH save method.
                searchResult = SearchArrayInArray(fileDataArray, StringToBytes("FLASH"));


                if (searchResult > 0)
                {
                    saveMethodFound = true;

                    //Build the name of the Flash method.
                    while (Char.IsLetterOrDigit(Convert.ToChar(fileDataArray[searchResult + tempInt])) || Char.IsPunctuation(Convert.ToChar(fileDataArray[searchResult + tempInt])))
                    {
                        saveType += Convert.ToChar(fileDataArray[searchResult + tempInt]);
                        tempInt += 1;
                    }

                    // Add "----" and position of the word to the save type string.
                    saveType += ("----" + searchResult.ToString());
                }
            }

            #endregion

            #region NO SAVE METHOD

            // If none of the searches gave a succesfull result
            if (!saveMethodFound)
            {
                // No save type?
                saveType = "No save?" + "----" + searchResult.ToString();
            }

            #endregion

            #region RETURN VALUE

            return saveType;

            #endregion
        }



        /// <summary>
        /// This method accomplish a complementary check on the rom.
        /// </summary>
        /// <param name="array">The header data array or ROM data array</param>
        /// <returns>The value of the check (number between 0 and 256)</returns>
        public static int ComplementaryCheck(byte[] array)
        {
            #region LOCAL ATTRIBUTES

            // Temporary hex value
            string tempHex;

            // Starting decimal value
            int value = 25;

            #endregion

            #region COMPLEMENTARY CHECK

            // Add 29 bytes to value (Bytes 160 to 188)
            for (int i = 160; i <= 188; i++)
            {
                value += array[i];
            }

            // Convert value to hex.
            tempHex = convertfrombase10(value);

            // Convert the last char of tempHex into a int32
            value = convertfrombase16(tempHex.Substring(tempHex.Length - 2));

            value = 256 - value;

            #endregion

            #region RETURN VALUE

            return value;

            #endregion
        }

        # endregion
    }
}
