using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realsoft_ComunicaBalanca
{
    public class Balanca
    {
        public static SerialPort _serialPort = new SerialPort();

        private static string retorno;
        public static void OpenPorta(string porta)
        {
            if (porta != "")
            {

                try
                {
                     _serialPort.PortName = porta;
                    _serialPort.BaudRate = 2400;
                    _serialPort.Parity = Parity.None;
                    _serialPort.DataBits = 8;
                    _serialPort.StopBits = StopBits.One;
                    _serialPort.ReadTimeout = 500;
                    _serialPort.WriteTimeout = 500;
                    _serialPort.Open();
                    
                }
                catch (Exception ex)
                {
                    throw new Exception("PROBLEMA AO ACESSAR A PORTA SERIAL " );
                }

            }

        }
        public static void Pede()
        {
            try
            {

                _serialPort.Write(Convert.ToString((char)5));

                System.Threading.Thread.Sleep(250);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public static double Recebe()
        {
            try
            {
                string ret = "";
                string ret1 = "";
                string ret2 = "";


                byte[] data = new byte[_serialPort.BytesToRead];
                _serialPort.Read(data, 0, data.Length);

                string EntradaStr = "";
                foreach (byte b in data)
                {
                    if (b <= 57)
                    {
                        if (b >= 48)
                        {
                            char Letra = Convert.ToChar(b);
                            EntradaStr = EntradaStr + Convert.ToString(Letra);
                        }
                    }
                }

                if (EntradaStr.Length > 4)
                {
                    ret = EntradaStr.Insert(EntradaStr.Length - 3, ".");

                }
                

                _serialPort.Close();

                return double.Parse(EntradaStr.Insert(EntradaStr.Length - 3, ","));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }
    }
}
