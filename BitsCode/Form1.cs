using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BitsCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private string CreateMes(int num_letters)
        {
            Random rnd = new Random();
            

            // Создаем массив букв, которые мы будем использовать.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopasdfghjklzxcvbnm;'-=[]./!,".ToCharArray();

            // Создаем генератор случайных чисел.
            Random rand = new Random();

                string word = "";
                for (int j = 1; j <= num_letters; j++)
                {

                    int letter_num = rand.Next(0, letters.Length - 1);

                    // Добавить письмо.
                    word += letters[letter_num];
                }
            return word;

            }
        private StringBuilder Coding(string message)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in System.Text.Encoding.Unicode.GetBytes(message))
                sb.Append(Convert.ToString(b, 2));


            return sb;
        }
            private void ChekingBoard()
            {
            
                double p = 0.005;
                int d;
                int t;
                int num_letters = 5;
                int n=0;
                double result=1;
                string message;
                int k = 0;
                double speed=0;
                int counter = 3;
                double P = 0.00001;

                int counter1 = 0;
                int counter2 = 0;

            while (true)
           {
                message = CreateMes(num_letters);
               // k = message.Length;

                
                StringBuilder sb = new StringBuilder();
              
                sb= Coding(message);
                string bits = sb.ToString();
                k = bits.Length;
                if(k<200)
                {
                    while (k < 600)
                    {
                        num_letters++;
                        message = CreateMes(num_letters);
                        k = message.Length;
                        sb= Coding(message);
                        bits = sb.ToString();
                        k = bits.Length;
                    }
                }

                bits = CreateCode(bits,k);

                char[] bits1 = bits.ToCharArray();
                n = bits1.Length;
                char[] bitsDist=new char[bits1.Length];
                for (int i=0;i<bits1.Length;i++)
                {
                    bitsDist[i] = bits[i];
                }
                int r = n - k;

                bitsDist = Distortion(bits1,bitsDist, n, counter);
                d = DistanceHm(n, bits1, bitsDist);
                

                t = (d - 1) / 2;
                result =Error(n, d, t, p);
               
                speed = Convert.ToDouble(k) / Convert.ToDouble(n);
                if (result <= P)
                {
                    if (r >= (int)GilbertBoard(d,n))
                    {
                        richTextBox1.Text+= "n= " + n + " ;";
                        richTextBox1.Text+= "k= " + k + " ;";
                        richTextBox1.Text+= "d= " + d + " ;\n";
                        richTextBox2.Text+= "R= " + speed + " ; \n";
                        counter1++;

                    }
                    if (r <(int) GilbertBoard(d,n))
                    {
                        richTextBox3.Text += "n= " + n + " ;";
                        richTextBox3.Text += "k= " + k + " ;";
                        richTextBox3.Text += "d= " + d + " ;\n";
                        richTextBox4.Text += "R= " + speed + " ; \n";
                        counter2++;
                    }

                }


                num_letters++;


                counter = n / 200;

                   
                  
                
            }
    }


            private double GilbertBoard(int d,int n)
            {
                double res = 0;

                for (int i = 0; i <(d-2); i++)
                {
                    res +=(double) (factorial(n-1) / (factorial(n-1 - i) * factorial(i)));
                }


                return Math.Log(res, 2);
            }

            static BigInteger factorial(BigInteger x) { return x <= 1 ? 1 : x * factorial(x - 1); }

            private double Error(int n, int d, int t, double p)
            {
               double result=0;
               double combin = 0;
            

            
            //   for (int i = 0; i <= t; i++)
              //  {
                    combin = (double)(factorial(n) / (factorial(n - 1) * factorial(1)));
                    result =Math.Pow(p, 1) * Math.Pow((1 - p), (n - 1)) * combin; 

            //    }
                return result;
            }

            private int DistanceHm(int n, char[] bits, char[] bitsDist)
            {
                int d = 0;
                for (int i = 0; i < n; i++)
                {
                    if (bits[i] != bitsDist[i])
                    {
                        d++;
                    }
                }
                return d;
            }

            private char [] Distortion(char [] bits,char[] bitsDist, int n, int counter)
            {
                Random rnd = new Random();
                for (int i = 0; i < counter; i++)
                {
                foreach (byte b in System.Text.Encoding.Unicode.GetBytes(rnd.Next(0, 2).ToString()))
                    bitsDist[i] = Convert.ToChar(b);
                if (bits[i]==bitsDist[i])
                {

                    for(; ;)
                     
                    {
                        byte [] b = System.Text.Encoding.Unicode.GetBytes(rnd.Next(0, 2).ToString());
                        if (b[0] != bitsDist[i])
                        {
                            bitsDist[i] = Convert.ToChar(b[0]);
                            break;

                        }
                           
                         

                    }   
                }

                }


                return bitsDist;
            }
       
        private string CreateCode(string bits, int k)
        {
            int pow = 0;
            int i = 0;
            for (i=0;;i++)
            {
                
                pow=(int)Math.Pow(2, i);
                if(pow>=k)
                {
                    i--;
                    break;
                }
                else
                    { string s = '0'.ToString();
                bits = bits.Insert(pow,s); }
               

            }

            return bits;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ChekingBoard();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    }
