using System.Drawing;
using System.Drawing.Imaging;
/* careful with off by one errors! in the declaration of an array, in the last pixel of a bitmap. both count from the 0th index.
* 
* 
* 
* 
*/
public class key
{
    public int index = 0;
    public int value;
}
class MyClass
{


    static void Main()
    {
        string retrunfilepath = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\results\Result" + DateTime.Now.ToBinary(); 
        retrunfilepath = $@"..\..\..\Result\obr2-barcelona-park-result" + DateTime.Now.ToBinary();
        /*
        string filepath0 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr1-terc\b-terc.jpg";
        string filepath1 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr1-terc\c-terc.jpg";
        string filepath2 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr1-terc\d-terc.jpg";
        string filepath3 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr1-terc\z-terc.jpg";
        string filepath0 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr2-barcelona-park\0-barcelona-park.jpg";
        string filepath1 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr2-barcelona-park\1-barcelona-park.jpg";
        string filepath2 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr2-barcelona-park\2-barcelona-park.jpg";
        string filepath3 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr2-barcelona-park\3-barcelona-park.jpg";
        */
        string[] filepath = new string[4];
        for (int i = 0; i < 4; i++)
        {
            filepath[i] = $@"..\..\..\obr2-barcelona-park\{i}-barcelona-park.jpg";       
        }
        /*
       string filepath0 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr3-cz-pl-de\0-cz-pl-de.jpg";
       string filepath1 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr3-cz-pl-de\1-cz-pl-de.jpg";
       string filepath2 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr3-cz-pl-de\2-cz-pl-de.jpg";
       string filepath3 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr3-cz-pl-de\3-cz-pl-de.jpg";

       string filepath0 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr4-ups\0-ups.jpg";
       string filepath1 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr4-ups\1-ups.jpg";
       string filepath2 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr4-ups\2-ups.jpg";
       string filepath3 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr4-ups\3-ups.jpg";

       string filepath0 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr5-hracholusky\0-hracholusky.jpg";
       string filepath1 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr5-hracholusky\1-hracholusky.jpg";
       string filepath2 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr5-hracholusky\2-hracholusky.jpg";
       string filepath3 = @"C:\Users\sichr_dwu16fi\Desktop\MiniPuzzle\obr5-hracholusky\3-hracholusky.jpg";
       */
        Bitmap[] BitmapArray = new Bitmap[4];
        BitmapArray[0] = new Bitmap(filepath[0]);
        BitmapArray[1] = new Bitmap(filepath[1]);
        BitmapArray[2] = new Bitmap(filepath[2]);
        BitmapArray[3] = new Bitmap(filepath[3]);
        int SimilarityCoefficient = 8;
        foreach (var item in BestMatch(4, BitmapArray, SimilarityCoefficient))
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("at precision {0} the similarity is {1}/{2}", SimilarityCoefficient, (CompareAllSides(BitmapArray[BestMatch(4, BitmapArray, SimilarityCoefficient)[0] - 1], BitmapArray[BestMatch(4, BitmapArray, SimilarityCoefficient)[1] - 1], BitmapArray[BestMatch(4, BitmapArray, SimilarityCoefficient)[2] - 1], BitmapArray[BestMatch(4, BitmapArray, SimilarityCoefficient)[3] - 1], SimilarityCoefficient)), (2 * BitmapArray[0].Height + 2 * BitmapArray[0].Width));
        JoinImages(BitmapArray[BestMatch(4, BitmapArray, SimilarityCoefficient)[0] - 1], BitmapArray[BestMatch(4, BitmapArray, SimilarityCoefficient)[1] - 1], BitmapArray[BestMatch(4, BitmapArray, SimilarityCoefficient)[2] - 1], BitmapArray[BestMatch(4, BitmapArray, SimilarityCoefficient)[3] - 1]).Save(retrunfilepath  + ".jpg", ImageFormat.Jpeg);

        static Bitmap JoinImages(Bitmap TL, Bitmap TR, Bitmap BL, Bitmap BR)
        {

            Bitmap NewImage = new Bitmap(TL.Width + TR.Width, TL.Height + BL.Height);
            Graphics.FromImage(NewImage).DrawImage(TL, 0, 0);
            Graphics.FromImage(NewImage).DrawImage(TR, TR.Width, 0);
            Graphics.FromImage(NewImage).DrawImage(BL, 0, BL.Height);
            Graphics.FromImage(NewImage).DrawImage(BR, BL.Width, TR.Height);

            return NewImage;
        }
        static int[] BestMatch(int digits, Bitmap[] BitmapArray, int SimilarityCoefficient)
        {
            key HighestKey = new key() { value = 0 };
            for (int i = 0; i < Factorial(digits); i++)
            {
                if (CompareAllSides(BitmapArray[GetNumOrderFromKey(digits, i)[0] - 1], BitmapArray[GetNumOrderFromKey(digits, i)[1] - 1], BitmapArray[GetNumOrderFromKey(digits, i)[2] - 1], BitmapArray[GetNumOrderFromKey(digits, i)[3] - 1], SimilarityCoefficient) >= HighestKey.value)
                {
                    HighestKey.index = i;
                    HighestKey.value = CompareAllSides(BitmapArray[GetNumOrderFromKey(digits, i)[0] - 1], BitmapArray[GetNumOrderFromKey(digits, i)[1] - 1], BitmapArray[GetNumOrderFromKey(digits, i)[2] - 1], BitmapArray[GetNumOrderFromKey(digits, i)[3] - 1], SimilarityCoefficient);
                }
            }
            return GetNumOrderFromKey(digits, HighestKey.index);
        }
        static int[] GetNumOrderFromKey(int digits, int Key)
        {
            Key = Key % Factorial(digits);
            int[] IntsInOrder = new int[digits];
            List<int> NumbersToOrder = new List<int>(); for (int i = 1; i <= digits; i++) { NumbersToOrder.Add(i); }
            for (int i = 0; i < digits - 1; i++)
            {
                IntsInOrder[i] = NthLowestNumber(NumbersToOrder, Convert.ToInt32(Math.Floor(((float)Key % Factorial(digits - i)) / Factorial(digits - i - 1)) + 1));
                NumbersToOrder.Remove(IntsInOrder[i]);
            }
            IntsInOrder[digits - 1] = NumbersToOrder[0];
            return IntsInOrder;
        }//up to 12!
        static int CompareAllSides(Bitmap TL, Bitmap TR, Bitmap BL, Bitmap BR, int SimilarityCoefficient)
        {
            int IdenticalPixels =
            CompareASide(TL, TR, "X+", SimilarityCoefficient) + CompareASide(TL, BL, "Y-", SimilarityCoefficient) + CompareASide(BL, BR, "X+", SimilarityCoefficient) + CompareASide(TR, BR, "Y-", SimilarityCoefficient);

            return IdenticalPixels;
        }
        static int CompareASide(Bitmap MapOne, Bitmap MapTwo, string SideOfOne, int SimilarityCoefficient)
        {
            //first 2 arguments are images, 3rd asks which side of the first image should be compared (with the opposite side of the 2nd image)
            int IdenticalPixels = 0;
            int XofOne = 0;
            int XofTwo = 0;
            int YofOne = 0;
            int YofTwo = 0;
            if (SideOfOne == "X+") { XofOne = MapOne.Width - 1; }
            else if (SideOfOne == "X-") { XofTwo = MapTwo.Width - 1; }
            else if (SideOfOne == "Y+") { SideOfOne = "Y"; YofTwo = MapTwo.Height - 1; }
            else if (SideOfOne == "Y-") { SideOfOne = "Y"; YofOne = MapOne.Height - 1; }
            else { XofOne = MapOne.Width - 1; }   //nothing will be taken as X+ or right side of map1 default

            if (SideOfOne == "Y")
            {
                for (int i = 0; i < MapOne.Width; i++)
                {
                    if (CompareTwoColors(MapOne.GetPixel(XofOne + i, YofOne), MapTwo.GetPixel(XofTwo + i, YofTwo), SimilarityCoefficient) == true)
                    {
                        IdenticalPixels++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < MapOne.Height; i++)
                {
                    if (CompareTwoColors(MapOne.GetPixel(XofOne, YofOne + i), MapTwo.GetPixel(XofTwo, YofTwo + i), SimilarityCoefficient) == true)
                    {
                        IdenticalPixels++;
                    }
                }
            }
            //compares one by one every pixel along the compared sides and tallies them in the return
            return IdenticalPixels;
        }
        static bool CompareTwoColors(Color ColorOne, Color ColorTwo, int SimilarityCoefficient)
        {
            bool AreSimilar = false;
            if (Math.Abs(ColorOne.A - ColorTwo.A) < SimilarityCoefficient)
            {
                if (Math.Abs(ColorOne.R - ColorTwo.R) < SimilarityCoefficient)
                {
                    if (Math.Abs(ColorOne.G - ColorTwo.G) < SimilarityCoefficient)
                    {
                        if (Math.Abs(ColorOne.B - ColorTwo.B) < SimilarityCoefficient)
                        {
                            AreSimilar = true;
                        }
                    }
                }
            }
            return AreSimilar;
        }
        static int NthLowestNumber(List<int> List, int N)
        {
            /*
            for (int j = 0; j < List.Count(); j++)
            {
                for (int i = 0; i < List.Count() - j - 1; i++)
                {
                    if (List[i] > List[i + 1])
                    {
                        int intsI = List[i];
                        List[i] = List[i + 1];
                        List[i + 1] = intsI;
                    }
                }
            }
            */
            try
            {
                return List[N - 1];
            }
            catch
            {
                return List[0];
            }

        }
        static int Factorial(int Number)
        {
            int Factorial = 1;
            for (int i = 1; i <= Number; i++)
            {
                Factorial = Factorial * i;
            }
            return Factorial;
        }
    }
}