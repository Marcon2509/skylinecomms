using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BitRateCalc {

    public class Program{
        public static void Main(string[] args) {
            CheckIfJsonExists();
        }

    public static void CheckIfJsonExists() {
        if (File.Exists("input.json")) {
            LoadJsonFile();
        } else {
            Console.WriteLine("missing input json file!");
        }
    }

    private static void LoadJsonFile()
    {
        string jsonText = File.ReadAllText("input.json");
        var data  = Newtonsoft.Json.JsonConvert.DeserializeObject<Input>(jsonText);
        // Console.WriteLine(data.device);
        // Console.WriteLine(data.model);
        NIC[] nicValData = data.nic;
        // Console.WriteLine(nicValData[0].wee);
        // Console.WriteLine(nicValData[0].mac);
        // Console.WriteLine(nicValData[0].timestamp);
        // Console.WriteLine(nicValData[0].rx);
        // Console.WriteLine(nicValData[0].tx);
        if (nicValData[0].rx.HasValue && nicValData[0].tx.HasValue) {
            double rxVal = nicValData[0].rx.Value;
            double txVal = nicValData[0].tx.Value;
            double result = getBitRate(rxVal, txVal);
            Console.WriteLine(string.Format("Requested BitRate : {0:0.0##}", result));
        } else {
            Console.WriteLine("Rx and/or tx value is null");
        }
        
    }

    private static double getBitRate(double rx, double tx)
    {
        //Im not sure if the formula is correct. I spent a long time struggling to find a fomula so hopefully this is correct
        //Bit rate = bandwidth / 8
        //bandwidth = (rx -tx) / (1 / polling)
        double sampleRate = 2.0;
        double bitsPerSample = rx - tx;
        return (bitsPerSample * sampleRate) / 8.0;
    }
    }



public class Input {
    public string? device { get; set; }
    public string? model { get; set; }

    //public string? nic { get; set; }
    public NIC[] nic { get; set; } 
}


public class NIC {
    public string? wee { get; set; } //im not sure why but when i use desc instead of wee it output no text at all
    public string? mac { get; set; }
    public DateTime? timestamp { get; set; }
    public double? rx { get; set; }
    public double? tx { get; set; }
}
}


  



