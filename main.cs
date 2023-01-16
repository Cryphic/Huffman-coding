using System.Text;


namespace ConsoleApp7
{


    class Node
    {
        public char data;
        public int freq;
        public Node left, right;


        public Node(char data, int freq)
        {
            this.data = data;
            this.freq = freq;
            left = right = null;
        }
    }


    internal class Program
    {

        public static void LKoodit(Node root, string str, Dictionary<char, string> dict)
        {

            if (root == null)
            {
                return;
            }


            if (root.left == null && root.right == null)
            {

                dict.Add(root.data, str);
            }


            LKoodit(root.left, str + "0", dict);
            LKoodit(root.right, str + "1", dict);
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Teksti: ");
            string input = Console.ReadLine();

            //laske toistuvuudet
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            foreach (char c in input)
            {
                if (charCount.ContainsKey(c))
                {
                    charCount[c]++;
                }
                else
                {
                    charCount.Add(c, 1);
                }
            }
            //tulosta kirjainten toistuvuus
            foreach (KeyValuePair<char, int> kvp in charCount)
            {
                Console.WriteLine("{0} , {1}", kvp.Key, kvp.Value);
            }


            //luo node merkeistä johon laitetaan kirjain ja toistuvuus
            List<Node> nodes = new List<Node>();
            foreach (KeyValuePair<char, int> kvp in charCount)
            {
                nodes.Add(new Node(kvp.Key, kvp.Value));
            }


            //otetaan kaks pienimpää nodea listasta
            while (nodes.Count > 1)
            {
                
                nodes.Sort((x, y) => x.freq.CompareTo(y.freq));

                //ekat 2
                List<Node> taken = nodes.GetRange(0, 2);

                //luo uusi node johon laitetaan 2 pienintä nodea
                Node parent = new Node('\0', taken[0].freq + taken[1].freq);
                parent.left = taken[0];
                parent.right = taken[1];

                //poista listasta otetut
                nodes.RemoveRange(0, 2);

                //Lisää uusi takaisin
                nodes.Add(parent);
            } //Viimeinen node loopista on puun alku.

            Node root = nodes.FirstOrDefault();


            Dictionary<char, string> codes = new Dictionary<char, string>();

            //tulosta koodit käymällä puu läpi 
            LKoodit(root, string.Empty, codes);

            //tulosta koodit
            foreach (KeyValuePair<char, string> kvp in codes)
            {
                Console.WriteLine("K: {0}, Ko: {1}", kvp.Key, kvp.Value);
            }

            //tulosta koodattu teksti
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                sb.Append(codes[c]);
            }

            Console.WriteLine("koodattu: " + sb.ToString());

            //'sb' dekoodaus käymällä nodemalli läpi
            StringBuilder output = new StringBuilder();
            Node current = root;
            foreach (char c in sb.ToString())
            {
                if (c == '0')
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }

                if (current.left == null && current.right == null)
                {
                    output.Append(current.data);
                    current = root;
                }
            }

            Console.WriteLine("dekoodattu: " + output.ToString());



        }
    }

}