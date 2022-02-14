using System;

namespace tr
{
    class Program
    {
        
        //Este código tem como objetivo aplicar a metaheurísticas "pesquisa tabu" para otimizar
        //um problema de localização
        //Esse problema consite em identificar de cinco centros de saude, quais devem abrir para
        //satisfazer as necessidades da população (20 povoações) de modo apurar o menor custo possivel
        //Salientar que devem estar pelo menos dois centros abertos.   


        //Esta função (calcularCustoCentros) recebe um vetor a indicar os centros que 
        //devem abrir e fechar e calcula o custo de abertura dos centros e retorna esse valor; 
        public static int calcularCustoCentros(int [] centros){

        //vetor com custo dos centro dado no enunciado    
        int[] CustosCentros = new int [5] {130, 180, 230, 150, 120 };
        int[] resultado =new int [5];
        resultado=centros;
        int custo=0;
        //loop for multiplica vetor dos custo com o vetor que indica os que abrem
        for (int i = 0; i < CustosCentros.Length; i++)
        {
            custo+=resultado[i]*CustosCentros[i];
        }
        //custo guarda o valor soma dos custos de abertura dos centros que abrem
        return (custo);

        }

        //Esta função (calcularCustoTotal) recebe uma matriz a indicar o centro que cada povoação
        //deve estar inscrita e calcula o custo da deslocação de todos os individuos
        //e o custo de abertura dos centros a onde as pessoas estão inscritas, soma os dois
        //valores e retorna a soma
        public static int calcularCustoTotal(int [,] matriz){
        
          
            //custo de cada povoação para cada centro
            int[,] matriz_custos = new int[20,5] { {9, 9, 14, 20, 30 },
                                            {12, 4, 22, 7, 11 },
                                            {22, 14, 19, 3, 20 },
                                            {30, 17, 28, 17, 13 },
                                            {15, 6, 20, 22, 23 },
                                            {15, 19, 15, 16, 26 },
                                            {9, 19, 29, 29, 28 },
                                            {28, 5, 10, 30, 30 },
                                            {12, 24, 10, 23, 11 },
                                            {10, 12, 12, 7, 22 },
                                            {4, 12, 14, 22, 15 },
                                            {15, 17, 15, 27, 9 },
                                            {5, 27, 30, 7, 23 },
                                            {11, 27, 29, 10, 22 },
                                            {13, 28, 28, 26, 17 },
                                            {6, 25, 13, 17, 28 },
                                            {4, 26, 24, 16, 12 },
                                            {28, 20, 18, 19, 5 },
                                            {21, 7, 9, 9, 6 },
                                            {28, 24, 17, 21, 6 } };
            //vetor com o custo de abertura de cada centro
            int[] CustosCentros = new int [5] {130, 180, 230, 150, 120 };

            //vetor que irá guardar variáveis binárias a indicar que centros abrem    
            int[] AbrirCentros = new int [5] {0,0,0,0,0};

            //codigo que apartir da matriz recebida determina que centros estão aberto e fechados 
            int k=1;
            foreach (int i in matriz)
            {
            if(k==1){
                if(i==1){AbrirCentros[k-1]=1;}
            }
            if(k==2){
                if(i==1){AbrirCentros[k-1]=1;}
            }
            if(k==3){
                if(i==1){AbrirCentros[k-1]=1;}
            }
            if(k==4){
                if(i==1){AbrirCentros[k-1]=1;}
            }
            if(k==5){
                if(i==1){AbrirCentros[k-1]=1;}
                k=0;
            }
            k++;
            }


            //calcula o custo de deslocação de todas as povoações com a solução inicial estabelecida
            //e guarda o valor desse custo
            int custoDeslocacao=0;
            for (int i = 0; i < 20; i++){
                for (int l = 0; l < 5; l++){     
                custoDeslocacao+=matriz[i,l]*matriz_custos[i,l]; 
                }
            }

            //calcula o custo de abertura dos centros chamando a função calcularCustoCentros e enviando 
            //o vetor acima apurado com os centros que abrem e que fecham
            int custoAbertura=calcularCustoCentros(AbrirCentros);
                
                
            //apurar o custo total somando custo de deslocação com custo de abertura
            int custoTotal=custoAbertura+custoDeslocacao;
            //retorna custo de abertura dos centros e o custo de deslocação das povoações
            return(custoTotal);
             
        }

        //Esta função calcularSolucaoIdeal recebe a solução inicial (que centros devem abrir) 
        //recebe também o numero de iterações pretendidas e o numero de elementos 
        //pretendidos na lista Tabu
        public static int [] calcularSolucaoIdeal(int[] solucaoInicial,int numeroIteracoes,int tamListaTabu){
            
            //lista que vai servir de auxilo à escolha do melhor solução
            int[,] intervalo = new int [5,5];
            //vetor que guardará a melhor solução de todas
            int[] melhorSolucao = new int [5]{1,1,1,1,1};
            //matriz que guardará lista Tabu com numero de elementos dinâmico 
            int [,] listaTabu = new int [tamListaTabu,5];
            //vetor que guardará melhor solução a partir da anterior que irá para a lista tabu
            int [] melhorVizinho=new int [5];

            melhorVizinho=solucaoInicial; 
                                               
            //contador que servirá de auxilo para adicionar elementos na lista tabu
            int contador=tamListaTabu; 

            //ciclo for vai executar um loop do numero de iterações 
            //pretendidas                                 
            for (int vl = 0; vl < numeroIteracoes; vl++)
            {
                //ciclos for que constroem uma matriz com as linhas repetidas
                //essas linhas são iguais ao vetor que guarda
                // a melhor solução a partir da anterior 
                for (int n = 0; n < 5; n++)
                {
                    for (int s = 0; s < 5; s++)
                    {
                        intervalo[n,s]=melhorVizinho[s];
                    }
                }

                //matriz altera a diagonal principal (se 0 = 1, se 1 = 0)
                //determinando assim novas soluções 
                for (int n = 0; n < 5; n++)
                {
                    for (int s = 0; s < 5; s++)
                    {
                        if(s==n){
                            if(intervalo[n,s]==0) {intervalo[n,s]=1;}
                            else{intervalo[n,s]=0;}
                        }                  
                    }
                }

                //vetor que guardará 1 a 1 todas as novas soluções
                int [] vizinho= new int [5];
                
                //antes ciclo que determinha a melhor solução apartir da solução anterior o vetor
                //melhorVizinho é corrigido para o pior para assim guarda o melhor de proxima "rodada"
                melhorVizinho=new int [5]{1,1,1,1,1}; 

                //ciclo for que vai calcular a melhor solução a partir da anteriora   
                for (int n = 0; n < 5; n++)
                {   
                    //ciclo for que guarda no vetor vizinho todas as soluções que 
                    //tem origem no solução anteriora retirando da matriz das soluções uma a uma
                    //ciclo for tambem verifica quantos centros abertos implicam essas soluções
                    int requisitoMin2centros=0;
                    for (int s = 0; s < 5; s++)
                    {
                        if(intervalo[n,s]==1){requisitoMin2centros++;}
                        vizinho[s]=intervalo[n,s];
                        
                    }

                    //if que valida se a solução retirada da matriz preenche o requisito
                    // de no mínimo ter 2 centros aberto
                    if(requisitoMin2centros>=2){

                        // vetor que irá guardar elemento a elemento da lista tabu para comparar
                        //com as soluções
                        int [] verificarListaTabu= new int[5];
                        bool pertenceListaTabu=false;

                        //ciclos for que irão identificar se a solução já está na lista tabu
                        for (int za = 0; za < tamListaTabu; za++)
                        {   
                            int cont=0;
                            //for que vai verificar se a solução existe na lista tabu;
                            for (int aa = 0; aa < 5; aa++)
                            {
                                //se este if se verificar 5 vezes quer dizer que retem 5 numero
                                //logo a solução está na lista tabu
                               if(vizinho[aa]==listaTabu[za,aa]){
                                    cont++;
                                } 
                                if (cont==5)
                                {
                                   pertenceListaTabu=true;
                                }
                            }
                                
                        }    
                        
                        //if que impede que solução que pertença à lista tabu seja considerada
                        if(pertenceListaTabu==false){

                            //criar a matriz a partir dos vetores para depois comparar os custos 
                            //totais dessas matrizes com as soluções encontradas
                            int [,] matrizVizinho = dividirPovoacao(vizinho);
                            int [,] matrizMelhorSolucao=dividirPovoacao(melhorSolucao);
                            int [,] matrizMelhorVizinho=dividirPovoacao(melhorVizinho);
                            //if que calcula custo da melhorsolução de todas encontrada 
                            //até agora e compara com a solução se for superir
                            //esta solução passa a ser a melhor
                            if(calcularCustoTotal(matrizVizinho)<calcularCustoTotal(matrizMelhorSolucao)){
                                for (int d = 0; d < melhorSolucao.Length; d++)
                                {
                                    melhorSolucao[d]=vizinho[d];
                                }
                            }
                            
                            //if que calcula custo da melhorsolução encontrada a partir da solução 
                            //anterior e compara com a solução se for superir
                            //esta solução passa a ser a melhor que surgiu apartir da anterior
                            if(calcularCustoTotal(matrizVizinho)<calcularCustoTotal(matrizMelhorVizinho)){
                                for (int k = 0; k < 5; k++)
                                {
                                    melhorVizinho[k]=vizinho[k];
                                }
                            }

                                                   
                        } 
                    }  
                }

                //if e for que ficam responsaveis por gerir o processo de registo na lista tabu
                //se for a primeira iteração a primeira linha da lista tabu = solução inicial
                contador++;
                if(vl==0){
                    for (int a = 0; a < 5; a++)
                    {
                        listaTabu[0,a]=solucaoInicial[a];
                    }
                }

                //for que gere o local onde será registada a solução na lista tabu
                //ex: iteração 5 e tamanho de lista lista tabu = 3
                // 5+3=8   8%3=2
                // solução da iteração 2 vai para tamListaTabu linha 3 ou tamListaTabu[2,a] 
                for (int a = 0; a < 5; a++)
                {
                        listaTabu[contador%tamListaTabu,a]=melhorVizinho[a];
                }
                //deste modo garantimos o modelo FIFO -> first in first out
            } 

            //questão de melhorar o output
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Lista Tabu com "+tamListaTabu+" elementos:");
            //exibe a lista tabu
            for (int pp = 0; pp < tamListaTabu; pp++)
            {
                for (int jj = 0; jj < 5; jj++)
                {
                    Console.Write(listaTabu[pp,jj]);
                }
                Console.WriteLine();
            }  
            
            Console.WriteLine();

            Console.WriteLine("Solução Ideal com "+numeroIteracoes+" iterações:");    
            for (int p = 0; p < 5; p++){   
                Console.Write(melhorSolucao[p]);                         
            }
            Console.WriteLine();
            Console.WriteLine();

            //retorna vetor com a melhor solução
            return melhorSolucao;

        }

        //Função que recebe um vetor com a solução e determina o custo de deslocação
        //de todas as povoações para os centros disponiveis indicando qual o mais barato
        //e deste modo para onde as povoações devem ir
        public static int [,] dividirPovoacao(int[] solucaoIdeal){
            //matriz com variáveis binárias que indicam o centro de cada povoação
            //matriz que guardará a solução final
            //inicalizada com todos os valores a 1 para ser considerada má
            //e ser atualizada para uma melhor 
            int[,] melhor_solucao = new int[20,5] {{1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },  
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 }, 
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 },
                                                  {1, 1, 1, 1, 1 } };
            //vetor que guardará as soluções todas e que comprarará com a melhor
            int[,] solucao_intermedia = new int[20,5];
                      
              

            int numeroDeCentrosAbertos=0;
            //determina o numero de centros abertos  
            for (int p = 0; p < 5; p++){                        
                numeroDeCentrosAbertos+=solucaoIdeal[p];
            }

            //ciclo for para percorrer a melhor matriz
            for (int i = 0; i < 20; i++){

                //sempre que mudamos de linha o vetor solucao_intermedia atualizada para
                //o melhor_solucao para que quando determinarmos as proximas linhas
                //a linha anterior esteja na melhor solução
                for (int vv = 0; vv < 20; vv++)
                {
                    for (int ss = 0; ss < 5; ss++)
                    {
                        solucao_intermedia[vv,ss]=melhor_solucao[vv,ss];
                    }
                }    

                //ciclo for que erá ser executado o numero de vezes igual ao numero de centros abertos
                //ex: a solução tem dois centros aberto
                //na primeira volta ele coloca o 1 no primeiro centro que surgir aberto e 0 nos restantes
                //na segunda volta ele bolqueia o primeiro centro e coloca no proximo centro que surgir
                //se houvesse terceira volta, bloqueava o segundo e coloca no proximo centro que surgir
                //assim sucessivamente
                for (int bn = 0; bn < numeroDeCentrosAbertos; bn++){
                    int kd=0;
                    for (int j = 0; j < 5; j++){
                        if(solucaoIdeal[j]==1){  
                            if(bn==kd){
                                solucao_intermedia[i,j]=1;
                            }
                            else{solucao_intermedia[i,j]=0;}
                        kd++;
                        }else{solucao_intermedia[i,j]=0;}
                    }

                     
                    //compra a solução com a melhor se inferior para essa solução a ser a melhor
                    if(calcularCustoTotal(solucao_intermedia)<calcularCustoTotal(melhor_solucao)){
                        for (int cv = 0; cv < 20; cv++)
                        {
                            for (int zv = 0; zv < 5; zv++)
                            {
                                melhor_solucao[cv,zv]=solucao_intermedia[cv,zv];
                            }
                        }

                    }

                }
                
            }
            //por fim a matriz já está alterada apresentando dos centros abertos as povoações associadas ao centro com menor custo de deslocação;
            //retorna matriz com a solução
            return melhor_solucao;       

        }

        //Função principal envia os dados necessario para as funções apresentadas funcionar
        //chamando-as assim
        static void Main(string[] args){
            
            //solução inicial
            //solução que o programa usará para determinar as proximas soluções
            int[] AbrirCentros = new int [5] {1,1,1,1,1};
            //numero de iterações que pretende-se realizar
            int numeroItercoes=3;
            //numero de elementos da lista tabu pretendidos
            int tamanhoListaTabu=4;
            //chamar a função calcularSolucaoIdeal enviando os dados em cima estabelecidos 
            AbrirCentros=calcularSolucaoIdeal(AbrirCentros,numeroItercoes,tamanhoListaTabu);
            //chamar a função dividirPovoacâo e enviar a solução ideal guardada na variavel
            //da linha anterior
            int [,] matrizFinal= dividirPovoacao(AbrirCentros);
            
            //apresentar o custo da menlho solução
            Console.WriteLine("Custo total com a melhor solução encontrada:");
            Console.WriteLine(calcularCustoTotal(matrizFinal));
            Console.WriteLine();

            //exibir a matriz que indica a que centro cada povoação deve ficar
            Console.WriteLine("Matriz que indica a que centro cada povoação deve ficar:");
            for (int b = 0; b < 20; b++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Console.Write(matrizFinal[b,c]);
                }
                Console.WriteLine();
            } 
        }
    }
}

