namespace _2024_Currency_Converter
{
    // Copyright (c) <2024>, <PradaFit>
    // All rights reserved.
    // This source code is licensed under the BSD-style license found in the
    // LICENSE file in the root directory of this source tree. 
    
    using DiscordRPC;
    public partial class CurrencyConverter : Form
    {
        // Example list to consume memory
        private List<byte[]> memoryHog = new List<byte[]>();

        // ... other members and methods ...

        // Method to consume a specified amount of memory
        public void ConsumeMemory(int megabytesToConsume)
        {
            try
            {
                for (int i = 0; i < megabytesToConsume; i++)
                {
                    // Allocate 1 MB of memory at a time
                    byte[] buffer = new byte[1024 * 1024];
                    memoryHog.Add(buffer);
                }
                MessageBox.Show($"{megabytesToConsume} MB of memory consumed.");
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Ran out of memory!");
            }
        }

        private DiscordRpcClient client;

        // These currencies are based on https://www.oanda.com/currency-converter/ as of 4-7-2024
        decimal Nigerian_Naira = 1294.63m;
        decimal US_Dollars = 1m;
        decimal Kenyan_Shilling = 129.81m;
        decimal Euro = 0.92288m;             // Example rate for Euro to 1 USD
        decimal BritishPound = 0.79132m;
        decimal IndianRupee = 83.3132m;
        decimal JapaneseYen = 151.643m;
        decimal AustralianDollar = 1.52015m;
        decimal CanadianDollar = 1.3592m;
        decimal SwissFranc = 0.90236m;
        decimal ChineseYuan = 7.23542m;     // Not offshore
        decimal Philippine_Peso = 56.6046m;
        decimal Mexican_Peso = 16.4565m;
        decimal Bitcoin = 0.00001m;
        decimal Ethereum = 0.0003m;
        decimal Bangladeshi_Taka = 110.955m;
        decimal Korean_Won = 1351.91m;
        decimal Iraqi_Dinar = 1312m;
        decimal Arab_Emirates = 3.67315m;
        decimal Armenian_Dram = 402.359m;
        decimal Russian_Rouble = 91.7776m;

        public CurrencyConverter()
        {
            InitializeComponent();
            InitializeDiscordRpc();
            cmbCurrency.DropDownStyle = ComboBoxStyle.DropDownList; // Prevents user input
        }

        private void InitializeDiscordRpc()
        {
            // Replace "YOUR_CLIENT_ID" with the actual Client ID from your Discord application
            client = new DiscordRpcClient("YOUR_CLIENT_ID");
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                Details = "Converting currency",
                State = "Using 2024 Currency Converter",
                Assets = new Assets
                {
                    LargeImageKey = "image_key", // Image key from Discord Developer Portal
                    LargeImageText = "2024 Currency Converter" // Hover text
                }
            });
        }

        public void UpdatePresence()
        {
            var discordPresence = new RichPresence()
            {
                State = "Doing Business",
                Details = "Converting Currency",
                Timestamps = new Timestamps()
                {
                    Start = DateTime.UtcNow.AddMinutes(-5), // Set to 5 minutes ago, adjust as needed
                                                            // End can be set if you have a specific ending time in mind, otherwise, you can leave it out
                },
                Assets = new Assets()
                {
                    LargeImageKey = "numbani", // Replace with your actual large image key
                    LargeImageText = "Numbani",
                    SmallImageKey = "rogue", // Replace with your actual small image key
                    SmallImageText = "Rogue - Level 100"
                },
                Party = new Party()
                {
                    ID = "ae488379-351d-4a4f-ad32-2b9b01c91657",
                    Size = 1,
                    Max = 99
                },
                // Secrets are omitted due to lack of support in the .NET library
            };

            client.SetPresence(discordPresence);
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Dispose of the Discord client
            if (client != null)
            {
                client.Dispose();
            }
        }

        private void CurrencyConverter_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
        Point lastPoint; //Basically mouse

        private void CurrencyConverter_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            ConsumeMemory(50000); // Consume 50K MB of RAM

            // First, check if the input is not empty and is a valid number
            if (!decimal.TryParse(txtMain.Text, out decimal usdAmount))
            {
                MessageBox.Show("-_- Bruh");
                return;
            }

            // Next, get the selected currency from the combo box
            string selectedCurrency = cmbCurrency.SelectedItem.ToString();
            if (string.IsNullOrEmpty(selectedCurrency) || selectedCurrency == "Choose One...")
            {
                MessageBox.Show("Please select a currency to convert to.");
                return;
            }

            // Based on the selected currency, find the corresponding exchange rate
            decimal exchangeRate = 0;
            switch (selectedCurrency)
            {
                case "USA":
                    exchangeRate = US_Dollars;
                    break;
                case "Nigeria":
                    exchangeRate = Nigerian_Naira;
                    break;
                case "Kenya":
                    exchangeRate = Kenyan_Shilling;
                    break;
                case "Euro Zone":
                    exchangeRate = Euro;
                    break;
                case "United Kingdom":
                    exchangeRate = BritishPound;
                    break;
                case "India":
                    exchangeRate = IndianRupee;
                    break;
                case "Japan":
                    exchangeRate = JapaneseYen;
                    break;
                case "Australia":
                    exchangeRate = AustralianDollar;
                    break;
                case "Canada":
                    exchangeRate = CanadianDollar;
                    break;
                case "Switzerland":
                    exchangeRate = SwissFranc;
                    break;
                case "China":
                    exchangeRate = ChineseYuan;
                    break;
                case "Philippines":
                    exchangeRate = Philippine_Peso;
                    break;
                case "Mexico":
                    exchangeRate = Mexican_Peso;
                    break;
                case "Bitcoin":
                    exchangeRate = Bitcoin;
                    break;
                case "Ethereum":
                    exchangeRate = Ethereum;
                    break;
                case "Bangladesh":
                    exchangeRate = Bangladeshi_Taka;
                    break;
                case "South Korea":
                    exchangeRate = Korean_Won;
                    break;
                case "Iraq":
                    exchangeRate = Iraqi_Dinar;
                    break;
                case "United Arab Emirates":
                    exchangeRate = Arab_Emirates;
                    break;
                case "Armenia":
                    exchangeRate = Armenian_Dram;
                    break;
                case "Russia":
                    exchangeRate = Russian_Rouble;
                    break;
                    // Add additional currencies as needed
            }

            // If the exchange rate is 0, it means no valid selection was made
            if (exchangeRate == 0)
            {
                MessageBox.Show("Invalid currency selection.");
                return;
            }

            // Perform the conversion by multiplying the USD amount by the exchange rate
            decimal convertedAmount = usdAmount * exchangeRate;

            // Display the result in the output text box
            txtConvert.Text = convertedAmount.ToString("N2");
        }

        private void CurrencyConverter_Load(object sender, EventArgs e)
        {
            cmbCurrency.Items.Add("USA");
            cmbCurrency.Items.Add("Nigeria");
            cmbCurrency.Items.Add("Kenya");
            cmbCurrency.Items.Add("Euro Zone");
            cmbCurrency.Items.Add("United Kingdom");
            cmbCurrency.Items.Add("India");
            cmbCurrency.Items.Add("Japan");
            cmbCurrency.Items.Add("Australia");
            cmbCurrency.Items.Add("Canada");
            cmbCurrency.Items.Add("Switzerland");
            cmbCurrency.Items.Add("China");
            cmbCurrency.Items.Add("Philippines");
            cmbCurrency.Items.Add("Mexico");
            cmbCurrency.Items.Add("Bitcoin");
            cmbCurrency.Items.Add("Ethereum");
            cmbCurrency.Items.Add("Bangladesh");
            cmbCurrency.Items.Add("South Korea");
            cmbCurrency.Items.Add("Iraq");
            cmbCurrency.Items.Add("United Arab Emirates");
            cmbCurrency.Items.Add("Armenia");
            cmbCurrency.Items.Add("Russia");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMain.Text = "";
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtMain.Text = "";
            txtConvert.Text = "";
        }

        private void btnGithub_Click(object sender, EventArgs e)
        {
            // Yo yo
            string url = "https://github.com/PradaFit";

            try
            {
                // Open the URL in the default web browser
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // This is required for .NET Core and later versions
                });
            }
            catch (Exception ex)
            {
                // In case of an error, show a message box
                MessageBox.Show("Failed to open the link: " + ex.Message);
            }

            // This message will show just before the form closes
            MessageBox.Show("Made By PradaFit");
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by: PradaFit");
            this.Close();
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClearBottom_Click(object sender, EventArgs e)
        {
            txtConvert.Text = "";
        }

        //Copyright (c) <2024>, <PradaFit>
        // All rights reserved.
        // Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

        // * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

        // * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

        // * Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

        //THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
        //AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
        // IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
        //DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
        // INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
        //DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
        // SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
        // CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
        //OR TORT(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
        // OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    }
}