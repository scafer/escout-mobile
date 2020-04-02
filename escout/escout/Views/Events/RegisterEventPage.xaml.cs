using escout.Helpers;
using escout.Models.Db;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace escout.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterEventPage : ContentPage
    {
        const string RecuperacaoDeBola = "Recuperação de bola";
        const string Interrupcao = "Interrupção";
        const string PerdaDeBola = "Perda de bola";
        const string Remate = "Remate";
        const string Passe = "Passe";
        const string PasseFalhado = "Passe falhado";
        const string PasseConcretizado = "Passe concretizado";
        const string AssistenciaSim = "Assistência Sim";
        const string AssistenciaNao = "Assistência Não";
        const string ParaFora = "Para fora";
        const string Intercetado = "Intercetado";
        const string ABaliza = "À baliza";
        const string BolaParada = "Bola parada";
        const string LancamentoAFavor = "Lançamento a favor";
        const string FaltaCometida = "Falta cometida";
        const string FaltaSofrida = "Falta sofrida";
        const string CartaoVermelho = "Cartão vermelho";
        const string CartaoAmarelo = "Cartão amarelo";
        const string AoPoste = "Ao poste";
        const string GuardaRedesDefendeu = "Guarda-Redes Defendeu";
        const string Golo = "Golo";
        const string Penalty = "Penalty";
        const string Livre = "Livre";
        const string Falhou = "Falhou";

        private const string Defesa = "Defesa";
        private const string PontapeDeBaliza = "Pontapé de baliza";
        private const string PosseDeBola = "Posse de bola";
        private const string BolaNaEquipaAdversaria = "Bola na equipa adversaria";
        private const string AgarrouBola = "Agarrou a bola";
        private const string NaoAgarrouBola = "Não agarrou a bola";

        private int type = 0;

        private DbGame dbGame;

        public RegisterEventPage()
        {
            InitializeComponent();
            this.dbGame = Utils.DbGame;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = Utils.DbGame;
            _ = BackgroundAsync();
            SetBoard1();
        }

        private async Task BackgroundAsync()
        {
            while (true)
            {
                LbStopwatch.Text = StopWatch.ShowTime();
                await Task.Delay(1000);
            }
        }

        private async void LeaveItem_OnClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            await Navigation.PushAsync(new MainPage());
        }


        private void Pause_Clicked(object sender, EventArgs e)
        {
            //StopWatch.Stop();
            type = 0;
            SetBoard1();
        }

        private void Resume_Clicked(object sender, EventArgs e)
        {
            //StopWatch.Start();
            type = 1;
            SetBoard1();
        }

        private void EventExecuted(object sender, EventArgs e)
        {
            var button = sender as Button;
            switch (button.Text)
            {
                case RecuperacaoDeBola:
                    SetBoard2();
                    break;
                case Interrupcao:
                    SetBoard5();
                    break;
                case PerdaDeBola:
                    SetBoard1();
                    break;
                case Remate:
                    SetBoard4();
                    break;
                case Passe:
                    SetBoard3();
                    break;
                case PasseFalhado:
                    SetBoard1();
                    break;
                case PasseConcretizado:
                    SetBoard11();
                    break;
                case AssistenciaSim:
                    SetBoard1();
                    break;
                case AssistenciaNao:
                    SetBoard1();
                    break;
                case ParaFora:
                    SetBoard1();
                    break;
                case Intercetado:
                    SetBoard1();
                    break;
                case ABaliza:
                    SetBoard7();
                    break;
                case BolaParada:
                    SetBoard8();
                    break;
                case LancamentoAFavor:
                    SetBoard2();
                    break;
                case FaltaCometida:
                    SetBoard6();
                    break;
                case FaltaSofrida:
                    SetBoard2();
                    break;
                case CartaoVermelho:
                    SetBoard1();
                    break;
                case CartaoAmarelo:
                    SetBoard1();
                    break;
                case AoPoste:
                    SetBoard1();
                    break;
                case GuardaRedesDefendeu:
                    SetBoard1();
                    break;
                case Golo:
                    SetBoard1();
                    break;
                case Penalty:
                    SetBoard10();
                    break;
                case Livre:
                    SetBoard9();
                    break;
                case Defesa:
                    SetBoard13();
                    break;
                case NaoAgarrouBola:
                    SetBoard12();
                    break;
                case AgarrouBola:
                    SetBoard2();
                    break;
                case PosseDeBola:
                    SetBoard2();
                    break;
                case BolaNaEquipaAdversaria:
                    SetBoard1();
                    break;
            }
        }

        private void SetBoard1()
        {
            Event1Button.Text = RecuperacaoDeBola;
            Event2Button.Text = Interrupcao;
            Event3Button.Text = "";
            Event4Button.Text = "";

            if (type == 1)
            {
                Event3Button.Text = Defesa;
            }
        }

        private void SetBoard2()
        {
            Event1Button.Text = Remate;
            Event2Button.Text = Interrupcao;
            Event3Button.Text = PerdaDeBola;
            Event4Button.Text = Passe;
        }

        private void SetBoard3()
        {
            Event1Button.Text = PasseFalhado;
            Event2Button.Text = PasseConcretizado;
            Event3Button.Text = "";
            Event4Button.Text = "";
        }

        private void SetBoard4()
        {
            Event1Button.Text = Intercetado;
            Event2Button.Text = BolaParada;
            Event3Button.Text = ParaFora;
            Event4Button.Text = ABaliza;
        }
        private void SetBoard5()
        {
            Event1Button.Text = FaltaCometida;
            Event2Button.Text = FaltaSofrida;
            Event3Button.Text = LancamentoAFavor;
            Event4Button.Text = "";

            if (type == 1)
            {
                Event4Button.Text = PontapeDeBaliza;
            }
        }

        private void SetBoard6()
        {
            Event1Button.Text = CartaoAmarelo;
            Event2Button.Text = CartaoVermelho;
            Event3Button.Text = "";
            Event4Button.Text = "";
        }

        private void SetBoard7()
        {
            Event1Button.Text = AoPoste;
            Event2Button.Text = GuardaRedesDefendeu;
            Event3Button.Text = Golo;
            Event4Button.Text = "";
        }

        private void SetBoard8()
        {
            Event1Button.Text = Penalty;
            Event2Button.Text = Livre;
            Event3Button.Text = "";
            Event4Button.Text = "";
        }

        private void SetBoard9()
        {
            Event1Button.Text = ParaFora;
            Event2Button.Text = ABaliza;
            Event3Button.Text = Intercetado;
            Event4Button.Text = "";
        }

        private void SetBoard10()
        {
            Event1Button.Text = Falhou;
            Event2Button.Text = Golo;
            Event3Button.Text = "";
            Event4Button.Text = "";
        }

        private void SetBoard11()
        {
            Event1Button.Text = AssistenciaNao;
            Event2Button.Text = AssistenciaSim;
            Event3Button.Text = "";
            Event4Button.Text = "";
        }

        private void SetBoard12()
        {
            Event1Button.Text = PosseDeBola;
            Event2Button.Text = Interrupcao;
            Event3Button.Text = BolaNaEquipaAdversaria;
            Event4Button.Text = "";
        }

        private void SetBoard13()
        {
            Event1Button.Text = NaoAgarrouBola;
            Event2Button.Text = AgarrouBola;
            Event3Button.Text = "";
            Event4Button.Text = "";
        }
    }
}