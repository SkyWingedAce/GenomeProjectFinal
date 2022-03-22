using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApplication2
{
    class Program
    {
        public class Genome
        {
            List<int> gene = new List<int>();
            Tuple<double, double> upLow;
            string genStr;
            

            public void fillList(int length)
            {
                Random r = new Random();
                for (int i = 0; i< length;i++)
                {  
                    gene.Add(r.Next(0, 2));
                }
                genStr = this.toString();
            }
            public string getReadableGene()
            {
                return genStr;
            }
            public List<int> getGene()
            {
                return gene;
            }
            public void setGene(List<int> newGene)
            {
                gene = newGene;
                genStr = this.toString();
            }
            public String toString()
            {
                String build = "";
                for (int i = 0; i < gene.Count; i++)
                {
                   build += gene[i].ToString();
                }
                return build;
            }
            public int getOnes()
            {
                int amount = 0;
                for(int i =0; i < gene.Count; i++ )
                {
                    int meT = gene[i];
                    if(meT == 1)
                    {
                        amount++;
                    }
                }
                return amount;
            }
            public Tuple<double, double> getUpLow()
            {
                return upLow;
            }
            public void setUpLow(Tuple<double, double> newUpLow)
            {
                upLow = new Tuple<double, double>(0, 0);
                upLow = newUpLow;
            }

        }

        static void Main(string[] args)
        {
            int Population = 0;
            double crossMe = 0;
            double mutation = 0;
            int genomLength = 0;
            string fileThingy = "";
            List<int> winners = new List<int>();
            Console.WriteLine(" Would you like to enter preferences? Y/N");
            string choice = Console.ReadLine();
            if (choice == "y" || choice == "Y")
            {
                Console.WriteLine("population must be an even number");
                Console.WriteLine("It is also recomended that population be 100 or greater");
                while (Population == 0)
                {
                    if (Int32.TryParse(Console.ReadLine(), out Population))
                    {
                        if( Population %2 !=0)
                        {
                            Population = 0;
                            Console.WriteLine(" number entered was not even please try again"); 
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input was invalid please try again");
                    }
                }
                Console.WriteLine("Cross over rate at which  genes will be spliced at a random location");
                Console.WriteLine("Rate must be a double between 0 and 1");
                while (crossMe == 0)
                {
                    if (Double.TryParse(Console.ReadLine(), out crossMe))
                    {
                        if (crossMe > 1 || crossMe < 0)
                        {
                            crossMe = 0;
                            Console.WriteLine(" number entered was not between 0 and 1");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input was invalid please try again");
                    }
                }
                Console.WriteLine("Rate at which a mutaiton can occur. This option must be between 0 and 1");
                Console.WriteLine("a high mutation rate may prevent a result");
                while (mutation == 0)
                {
                    if (Double.TryParse(Console.ReadLine(), out mutation))
                    {
                        if (mutation > 1 || mutation < 0)
                        {
                            mutation = 0;
                            Console.WriteLine(" number entered was not between 0 and 1");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input was invalid please try again");
                    }
                }
                Console.WriteLine("Genome Length: A low number is in advisable. This program will not accept a input under 3");
                while (genomLength == 0)
                {
                    if (Int32.TryParse(Console.ReadLine(), out genomLength))
                    {
                        if (genomLength < 3)
                        {
                            genomLength = 0;
                            Console.WriteLine("Number entered is not high enough");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input was invalid please try again");
                    }
                }

            }
            else if (choice == "n" || choice == "N")
            {
                Population = 100;
                crossMe = 0.7;
                mutation = 0.001;
                genomLength = 20;
            }
            else
            {
                while(choice != "y" || choice != "Y"|| choice != "n" || choice != "N")
                {
                    Console.WriteLine("please enter a proper response Y/N");
                    choice = Console.ReadLine();
                }
            }
            Console.WriteLine("would you like to output to a File? Y/N");
            choice = "";
            choice = Console.ReadLine();
            while (choice != "y" || choice != "Y" || choice != "n" || choice != "N")
            {
                if (choice == "n" || choice == "N")
                {
                    fileThingy = " ";
                    break;
                }
                else if (choice == "y" || choice == "Y")
                {
                    Console.WriteLine("please enter your file name.  .txt will be added to the name");
                    Console.WriteLine("File location by default is MyDocuments");
                    string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    fileThingy = (filepath + @"\" + Console.ReadLine() + ".txt");
                    break;
                }
                else
                {
                    while (choice != "y" || choice != "Y" || choice != "n" || choice != "N")
                    {
                        Console.WriteLine("please enter a viable anwser");
                        choice = Console.ReadLine();
                    }
                }
            }
            Console.WriteLine("Population size: " + Population + " Cross over: " + crossMe + " mutation:" + mutation + " genomeLength:" + genomLength + "File path:" + fileThingy);
            Console.WriteLine("if fileout is not displaying nothing was provided. please press enter to continue");
            Console.ReadLine();
            Program runMe = new Program();
            int generation = 0;
            generation = runMe.runGA(Population, crossMe, mutation, genomLength,fileThingy);

            /*
            do
            {
                generation = runMe.runGA(Population, crossMe, mutation, genomLength);
                winners.Add(generation);
            } while (winners.Count< 44);

            // adverage function
            int adverage = 0;
            for(int i = 0; i< winners.Count; i++)
            {
                adverage += winners[i];
            }
            adverage /= adverage / winners.Count();
            */


            Console.Write("Generation: " + generation);
            Console.WriteLine("PRogram has finished running please enter 1 to exit");
            int check = 0;
            check = 0;
            while (check != 1)
            {
                
                if (Int32.TryParse(Console.ReadLine(), out check))
                {
                    if (check != 1)
                    {                     
                        Console.WriteLine("Number entered is not correct");
                    }
                }
                else
                {
                    Console.WriteLine("Input was invalid please try again. Enter 1 to exit");
                }
            }

        }
        // creates a genom
        private Genome randomGenome(int length)
         {
            Genome temp = new Genome();
            temp.fillList(length);
            return temp;
         }
        // creats population
        private List<Genome> makePopulation(int size, int length)
         {
            List<Genome> pop = new List<Genome>();
            
            for (int i = 0; i < size; i++)
            {
                 pop.Add(randomGenome(length));
                if(i>0)
                {
                    List<int> temp = pop[i].getGene();
                    List<int> prev = pop[i-1].getGene();
                    if (temp.SequenceEqual(prev))
                    {
                        pop.Remove(pop[i]);
                        i--;
                    }
                }
            }
            
            return pop;
         }
        // fitness of a individual gene
        public double fitness(Genome calFit)
         {
            int temp;
            temp = calFit.getOnes();
            double me = (double)temp/20;
            return me;
         }
        //calculates mass fitness
        public Tuple<double, double> evaluateFitness(List<Genome> popul)
         {
            double generalFit = 0.0;
            double bestFit = 0.0;
            for (int i = 0; i < popul.Count; i++)
            {
                generalFit += fitness(popul[i]);
                if (fitness(popul[i]) > bestFit)
                {
                    bestFit = fitness(popul[i]);
                }
            }
            Tuple<double, double> thingy = new Tuple<double, double>(generalFit, bestFit);
            return thingy;
         }
        // single point cross over preformed at a random point and sending back 2 new genomes
        public Tuple<Genome, Genome> cross(Genome me1,Genome me2)
           {
            Random r = new Random();
            int me = r.Next(0, me1.getGene().Count);
            int length = me1.getGene().Count;
            List<int> temp, temp2, meGene, meGene2;
            temp = new List<int>();
            temp2 = new List<int>();
            meGene = me1.getGene();
            meGene2 = me2.getGene();

            //List<int> temp2;
            for(int i = me; i < length;i++)
            {
                temp.Add(meGene[i]);
                temp2.Add(meGene2[i]);
            }
            int j = 0;
            for (int i = me; i < length; i++)
            {
                meGene[i] = temp2[j];
                meGene2[i] = temp[j];
                j++;
            }
            me1.setGene(meGene);
            me2.setGene(meGene2);

            // i have implemented this in cross over so that if the same gene appears the cross over operation is not wasted and i get a unique gene from it
            if(me1.getGene().SequenceEqual(me2.getGene()))
            {
                me2 = randomGenome(me2.getGene().Count);
            }


            return new Tuple<Genome, Genome>(me1, me2);
          }

        // mutation method
        public Genome mutate(Genome mutatie, double mutation)
           {
            //Random r = new Random();
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
   
            List<int> temp = mutatie.getGene();
            Random r = new Random();
            int i = r.Next(0, mutatie.getGene().Count);
                var byteArray2 = new byte[8];
                provider.GetBytes(byteArray2);
                double tempB = BitConverter.ToDouble(byteArray2, 0);
                if (tempB < 0)
                {
                    tempB *= -1;
                }
                if (tempB > 1)
                {
                    do
                    {
                        tempB /= 10;
                    } while (tempB > 1);
                }
                if (tempB< mutation)
                {
                    if (temp[i] == 0)
                        temp[i] = 1;   
                    else
                        temp[i] = 0;
                }
            mutatie.setGene(temp);
            
            return mutatie;
          }
        
        //selects the two highest weighted objects
        public Tuple<Genome, Genome> selectPair(List<Genome> popul)
          {
            Genome me1 = new Genome(),meThe = new Genome();
            //this roulette wheel implementation 
            // takes in to account total fitness and determins relative fitness
            bool found1 = false, found2 = false;
            double fitNig = 0;
            double bestFit = evaluateFitness(popul).Item2;
            double totalFit = evaluateFitness(popul).Item1;
            for(int i = 0; i < popul.Count; i++)
            {
                Tuple<double, double> tempT = new Tuple<double, double>(fitNig, fitNig += (fitness(popul[i]) / totalFit));
                popul[i].setUpLow(tempT);
            }
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            if (bestFit >= .8)
            {
                //A problem i encountered in roulett wheel implementation is that
                //the pool can stagnate over time and simply become all of one gene type
                //which can remove cross over's ability  to edge closer to a all 1 string

                //so to prevent this i guarrentee 50% of the highest weight genes to carry over
                //so this implementation is half roulet wheel chosing and half elitism chosing
                //elitest chosen genes are removed from the poulation so they are not repeated
                for (int i = 0; i < popul.Count; i++)
                {
                    if (fitness(popul[i]) > bestFit)
                    {
                        // Console.WriteLine("finding the first genome");
                        bestFit = fitness(popul[i]);
                        me1 = popul[i];
                    }
                }
            }
            else
            {
                // after adjusting in cross over for same genes passed i reverted to the full roulett wheel implementation
                var byteArray = new byte[8];
                provider.GetBytes(byteArray);


                double tempD = BitConverter.ToDouble(byteArray, 0);
                if (double.IsNaN(tempD) == true)
                {
                    while (double.IsNaN(tempD) == true)
                    {
                        Random r = new Random();
                        tempD = r.NextDouble();
                    }
                }
                if (tempD < 0)
                {
                    tempD *= -1;
                }
                if (tempD > 1)
                {
                    do
                    {
                        tempD /= 10;
                    } while (tempD > 1);
                }
                do
                {
                    for (int t = 0; t < popul.Count; t++)
                    {
                        if (tempD > popul[t].getUpLow().Item1 && tempD < popul[t].getUpLow().Item2)
                        {
                            me1 = popul[t];
                            found1 = true;
                        }
                    }

                } while (found1 == false);
            }
 
            
            
            var byteArray2 = new byte[8];
            provider.GetBytes(byteArray2);

            double tempB = BitConverter.ToDouble(byteArray2, 0);
   //         Console.WriteLine(tempB);
           if(double.IsNaN(tempB) == true )
            {
                while (double.IsNaN(tempB) == true)
                {
                    Random r = new Random();
                    tempB = r.NextDouble();
                }
            }
            if (tempB < 0)
            {
                tempB *= -1;
            }
            if (tempB > 1)
            {
                do
                {
                    tempB /= 10;
                } while (tempB > 1);
                
            }
            //Console.WriteLine(tempB);
            do
            {
                //int t = g.Next(0, (popul.Count));
                for (int t = 0; t < popul.Count; t++)
                {
                    if (tempB > popul[t].getUpLow().Item1 && tempB < popul[t].getUpLow().Item2)
                    {
                      //  Console.WriteLine("finding the second genome");
                      if(popul[t].getGene().SequenceEqual(me1.getGene()))
                        {
                            byteArray2 = new byte[8];
                            provider.GetBytes(byteArray2);

                            tempB = BitConverter.ToDouble(byteArray2, 0);
                            //         Console.WriteLine(tempB);
                            if (double.IsNaN(tempB) == true)
                            {
                                while (double.IsNaN(tempB) == true)
                                {
                                    Random r = new Random();
                                    tempB = r.NextDouble();
                                }
                            }
                            if (tempB < 0)
                            {
                                tempB *= -1;
                            }
                            if (tempB > 1)
                            {
                                do
                                {
                                    tempB /= 10;
                                } while (tempB > 1);
                            }
                        }
                        else
                        {
                            //Console.WriteLine("found the second genome");
                            meThe = popul[t];
                            found2 = true;
                        }
                    }
                }
            } while (found2 == false);


            // this is an elitest implementation
            // it takes the most fit of the population and returns them
            // what this needs to work is to empty the previous list entierly
            // so upon completion remove the present objects from the original list
            /*
            double highest = fitness(me1), sndHigh = fitness(meThe),temp;
            for (int i = 1; i<popul.Count;i++)
            {
                temp = fitness(popul[i]);
                if(temp > highest)
                {
                    highest = temp;
                    meThe = me1;
                    me1 = popul[i];
                }
                else if(temp > sndHigh)
                {
                    sndHigh = temp;
                    meThe = popul[i];
                }
            }
            */
            return new Tuple<Genome, Genome>(me1, meThe);
        }
        //this is the main method
        private int runGA(int popSize, double crossOver, double mutateRate, int lengthG,string fileout)
          {
            int generation = 0;
            int winningGen = 0;
            //creation of population list
            List<Genome> populationList = makePopulation(popSize, lengthG);
            // main loop
            List<string> generations = new List<string>();
            while (winningGen < 1)
            {
                //if a generation has a fitness of 20 we found our generation
                Tuple<double, double> me = evaluateFitness(populationList);
                Console.WriteLine("Current Generation: " + generation + " generalfitness: " + me.Item1 + " BestFitness: " + me.Item2);
                string stringHolder = ("Current Generation: " + generation + " generalfitness: " + me.Item1 + " BestFitness: " + me.Item2);
                generations.Add(stringHolder);
                if (me.Item2 >= (double) 1)
                {
                    winningGen = generation;
                    //break;
                }
                 //Creates a new list based off roulette wheel choosing in select pair
                List<Genome> weightPopList = new List<Genome>();
                Tuple<Genome, Genome> toAdd;
                double bstFit = 0.0;
                int chg = 0;
                Genome holder = randomGenome(lengthG), sndHolder = randomGenome(lengthG);

                for (int x = 0; x < populationList.Count; x++)
                {
                    if (fitness(populationList[x]) > bstFit)
                    {
                        bstFit = fitness(populationList[x]);
                        holder = populationList[x];
                    }
                }
                populationList.Remove(holder);
                bstFit = 0;
                for (int x = 0; x < populationList.Count; x++)
                {
                    if (fitness(populationList[x]) > bstFit)
                    {
                        bstFit = fitness(populationList[x]);
                        sndHolder = populationList[x];
                    }
                }
                populationList.Remove(sndHolder);
                toAdd = new Tuple<Genome, Genome>(holder, sndHolder);

                RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
                var byteArray = new byte[8];
                provider.GetBytes(byteArray);


                double tempD = BitConverter.ToDouble(byteArray, 0);
                if (double.IsNaN(tempD) == true)
                {
                    while (double.IsNaN(tempD) == true)
                    {
                        Random r = new Random();
                        tempD = r.NextDouble();
                    }
                }
                if (tempD < 0)
                {
                    tempD *= -1;
                }
                if (tempD > 1)
                {
                    do
                    {
                        tempD /= 10;
                    } while (tempD > 1);

                }
                if (crossOver > tempD)
                {
                    toAdd = cross(toAdd.Item1, toAdd.Item2);
                    Genome temp = new Genome();
                    temp.setGene(toAdd.Item1.getGene());
                    weightPopList.Add(temp);

                    Genome temp2 = new Genome();
                    temp2.setGene(toAdd.Item2.getGene());
                    weightPopList.Add(temp2);
                }
                else
                {
                    Genome temp = new Genome();
                    temp.setGene(toAdd.Item1.getGene());
                    weightPopList.Add(temp);

                    Genome temp2 = new Genome();
                    temp2.setGene(toAdd.Item2.getGene());
                    weightPopList.Add(temp2);
                }

                    do {
                    toAdd = selectPair(populationList);
                        //detemns when to do a crossover
                        if (crossOver > tempD)
                        {
                        //Console.WriteLine("Cross over is happening");
                       // populationList.Remove(toAdd.Item1);
                        toAdd = cross(toAdd.Item1, toAdd.Item2);
                        Genome temp = new Genome();
                        temp.setGene(toAdd.Item1.getGene());
                        weightPopList.Add(temp);

                        Genome temp2 = new Genome();
                        temp2.setGene(toAdd.Item2.getGene());
                        weightPopList.Add(temp2);
                        }
                        else
                        {
                       // Console.WriteLine("Cross over did not happen");
                       // populationList.Remove(toAdd.Item1);
                        Genome temp = new Genome();
                        temp.setGene(toAdd.Item1.getGene());
                        weightPopList.Add(temp);

                        Genome temp2 = new Genome();
                        temp2.setGene(toAdd.Item2.getGene());
                        weightPopList.Add(temp2);
                        }              
                } while (weightPopList.Count != popSize);
                //Console.WriteLine("Setting new list");
                populationList = new List<Genome>();
                populationList = weightPopList;
                
                /*
                // I am implementing something to try to help the program allong
                //in this section if my best fitness is greater than .65 
                //this method will scan the population and any genes 
                //with a fitness of .25 or lower will be re-rolled
                double check = evaluateFitness(populationList).Item2;
                if ( check >= .55) {
                    for (int x = 0; x < populationList.Count; x++)
                    {
                        double hm = fitness(populationList[x]);
                        if (hm <= (double).5)
                        {
                            populationList[x] = randomGenome(lengthG);
                        }
                    }
                }
                */
                // This is implemented to force a mutation of objects that 
                // occur too many times in the population

            /*
               bstFit = 0.0;
               chg = 0;
               holder = randomGenome(lengthG);
                for (int x = 0; x < populationList.Count; x++)
                {
                    if (fitness(populationList[x]) > bstFit)
                    {
                        bstFit = fitness(populationList[x]);
                        holder = populationList[x];
                        chg = 0;
                    }
                    if (holder.getGene().SequenceEqual(populationList[x].getGene()))
                    {
                        chg++;
                    }
                
                }
                Console.WriteLine(holder.getReadableGene() + ": " + chg);
                */
                // This is implemented to force a mutation of objects that 
                // occur too many times in the population

                chg = 0;
                for (int i = 0; i<populationList.Count;i++)
                {
                    
                    for(int x = 0; x<populationList.Count; x++)
                    {
                        if(populationList[i].getGene().SequenceEqual(populationList[x].getGene()))
                        {
                            chg++;
                        }
                        if(chg >  15)
                        {
                            populationList[i] = randomGenome(lengthG);
                        }
                    }
 //                   Console.WriteLine(populationList[i].getReadableGene() + ": " + chg);
                }
                // runs the mutation method for everything
                for (int j = 0; j < populationList.Count; j++)
                {
                    //Console.WriteLine("attempting mutate: " + j);
                    populationList[j] = mutate(populationList[j], mutateRate);
                }

                generation++;
            }
            if (fileout != " ")
            {
                System.IO.StreamWriter fileXtra = new System.IO.StreamWriter(fileout);

                for (int i = 0; i < generations.Count; i++)
                {
                    string temp = generations[i];
                    fileXtra.WriteLine(temp);
                }
                fileXtra.Close();
            }
            

            return winningGen;
         }
     }
  }

