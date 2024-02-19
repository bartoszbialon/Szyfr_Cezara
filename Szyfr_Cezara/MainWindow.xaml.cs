using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Szyfr_Cezara
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void trimEncryptedText_Click(object sender, RoutedEventArgs e)
        {
            string inputText = inputBasicText.Text;
            string trimmedText = Regex.Replace(inputText, @"[^a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻxvqXVQ\s]", "");
            trimmedText = trimmedText.ToLower();

            encryptedText.Text = trimmedText;
            shiftOfEncryptedText.Text = "0";
        }

        private void DecreaseShiftEncryptedButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(shiftOfEncryptedText.Text, out int shift))
            {
                AlertText.Text = "";

                if (int.Parse(shiftOfEncryptedText.Text) > 0)
                {
                    shift--;
                    shiftOfEncryptedText.Text = shift.ToString();
                }
            }

            else
            {
                AlertText.Text = "Pole przesunięcie nie może zawierać liter alfabetu oraz innych znaków specjalnych!";
            }
        }

        private void IncreaseShiftEncryptedButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(shiftOfEncryptedText.Text, out int shift))
            {
                AlertText.Text = "";

                if (int.Parse(shiftOfEncryptedText.Text) < 34)
                {
                    shift++;
                    shiftOfEncryptedText.Text = shift.ToString();
                }
            }

            else
            {
                AlertText.Text = "Pole przesunięcie nie może zawierać liter alfabetu oraz innych znaków specjalnych!";
            }
        }

        private void Encryption_Click(object sender, RoutedEventArgs e)
        {
            string inputText = inputBasicText.Text;
            int shift;
            bool isShiftEncryptedNumber = int.TryParse(shiftOfEncryptedText.Text, out shift);

            if (inputText.Length > 0)
            {
                if (isShiftEncryptedNumber && (int.Parse(shiftOfEncryptedText.Text) >= 0))
                {
                    shift = int.Parse(shiftOfEncryptedText.Text);
                    string encrypted = Encrypt(encryptedText.Text, shift);

                    shiftOfDecryptedText.Text = shiftOfEncryptedText.Text;
                    decryptedText.Text = encrypted;

                    inputBasicText.Text = "";
                    encryptedText.Text = "";
                    shiftOfEncryptedText.Text = "";
                    AlertText.Text = "";
                }

                else
                {
                    AlertText.Text = "Nieprawidłowy format przesunięcia. Proszę wprowadzić liczbę całkowitą dodatnią!";
                }
            }

            else
            {
                AlertText.Text = "Brak szyfrowanego wyrazu. Proszę wprowadzić na początku szyfrowany wyraz!";
            }
        }

        private string Encrypt(string inputText, int shift)
        {
            StringBuilder result = new StringBuilder();

            foreach (char character in inputText)
            {
                if (char.IsLetter(character))
                {
                    char shiftedCharacter = (char)(character + shift);
                    char baseChar = char.IsUpper(character) ? 'A' : 'a';
                    shiftedCharacter = (char)(((shiftedCharacter - baseChar + 26) % 26) + baseChar);
                    result.Append(shiftedCharacter);
                }
                else
                {
                    result.Append(character);
                }
            }

            return result.ToString();
        }

        private void trimDecryptedText_Click(object sender, RoutedEventArgs e)
        {
            string decrypted = inputEncryptedText.Text;
            string trimmedDecrypted = Regex.Replace(decrypted, @"[^a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻxvqXVQ]", "");
            trimmedDecrypted = trimmedDecrypted.ToLower();

            decryptedText.Text = trimmedDecrypted;
            shiftOfDecryptedText.Text = "0";
        }

        private void DecreaseShiftDecryptedButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(shiftOfDecryptedText.Text, out int shiftDecrypted))
            {
                AlertText.Text = "";

                if (int.Parse(shiftOfDecryptedText.Text) > 0)
                {
                    shiftDecrypted--;
                    shiftOfDecryptedText.Text = shiftDecrypted.ToString();
                }
            }

            else
            {
                AlertText.Text = "Pole przesunięcie nie może zawierać liter alfabetu oraz innych znaków specjalnych!";
            }
        }

        private void IncreaseShiftDecryptedButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(shiftOfDecryptedText.Text, out int shiftDecrypted))
            {
                AlertText.Text = "";
                
                if (int.Parse(shiftOfDecryptedText.Text) < 34)
                {
                    shiftDecrypted++;
                    shiftOfDecryptedText.Text = shiftDecrypted.ToString();
                }
            }

            else
            {
                AlertText.Text = "Pole przesunięcie nie może zawierać liter alfabetu oraz innych znaków specjalnych!";
            }
        }

        private void Decryption_Click(object sender, RoutedEventArgs e)
        {
            string inputText = inputEncryptedText.Text;
            int shift;
            bool isShiftDecryptedNumber = int.TryParse(shiftOfDecryptedText.Text, out shift);

            if (inputText.Length > 0)
            {
                if (isShiftDecryptedNumber)
                {
                    shift = int.Parse(shiftOfDecryptedText.Text);
                    string decrypted = Decrypt(decryptedText.Text, shift);

                    shiftOfEncryptedText.Text = shiftOfDecryptedText.Text;
                    encryptedText.Text = decrypted;

                    inputEncryptedText.Text = "";
                    decryptedText.Text = "";
                    shiftOfDecryptedText.Text = "";
                    AlertText.Text = "";
                }

                else
                {
                    AlertText.Text = "Nieprawidłowy format przesunięcia. Proszę wprowadzić liczbę całkowitą dodatnią!";
                }
            }

            else
            {
                AlertText.Text = "Brak deszyfrowanego wyrazu. Proszę wprowadzić na początku deszyfrowany wyraz!";
            }
        }

        private string Decrypt(string inputText, int shift)
        {
            return Encrypt(inputText, -shift);
        }
    }
}