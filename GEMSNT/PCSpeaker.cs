using System;
using System.Collections.Generic;
using System.Text;

namespace GEMSNT.PCSpeaker
{
    public class PCSpeaker
    {
        //List of notes.
        public static int A0, AS0 = 28;
        public static int B0, C1, CS1 = 33;
        public static int D1, DS1 = 38;
        public static int E1, F1 = 42;
        //eh screw it. im done being lazy.
        public static int FS1 = 46;
        public static int G1 = 49;
        public static int GS1 = 52;
        public static int A1 = 55;
        public static int AS1 = 58;
        public static int B1 = 62;
        public static int C2 = 65;
        public static int CS2 = 69;
        public static int D2 = 73;
        public static int DS2 = 78;
        public static int E2 = 82;
        public static int F2 = 87;
        public static int FS2 = 92;
        public static int G2 = 98;
        public static int GS2 = 104;
        public static int A2 = 110;
        public static int AS2 = 117;
        public static int B2 = 123;
        public static int C3 = 131;
        public static int CS3 = 139;
        public static int D3 = 147;
        public static int DS3 = 156;
        public static int E3 = 165;
        public static int F3 = 175;
        public static int FS3 = 185;
        public static int G3 = 196;
        public static int GS3 = 208;
        public static int A3 = 220;
        public static int AS3 = 233;
        public static int B3 = 247;
        public static int C4 = 262;
        public static int CS4 = 277;
        public static int D4 = 294;
        public static int DS4 = 311;
        public static int E4 = 330;
        public static int F4 = 349;
        public static int FS4 = 370;
        public static int G4 = 392;
        public static int GS4 = 415;
        public static int A4 = 440;
        public static int AS4 = 466;
        public static int B4 = 494;
        public static int C5 = 523;
        public static int CS5 = 554;
        public static int D5 = 587;
        public static int DS5 = 622;
        public static int E5 = 659;
        //End of notes.
        public static void beep()
        {
            Console.Beep(1000, 530);
        }

        public static void play(int note, int ms)
        {
            Console.Beep(note, ms);
        }
    }
}
