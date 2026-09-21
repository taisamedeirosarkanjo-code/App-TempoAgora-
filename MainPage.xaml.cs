using Microsoft.Maui.Networking;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Verifica se tem conexão com a internet
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlert("Sem conexão",
                        "Você está sem conexão com a internet. Verifique sua rede e tente novamente.",
                        "OK");
                    return;
                }

                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = $"Latitude: {t.lat} \n" +
                                               $"Longitude: {t.lon} \n" +
                                               $"Nascer do Sol: {t.sunrise} \n" +
                                               $"Por do Sol: {t.sunset} \n" +
                                               $"Temp Máx: {t.temp_max} °C\n" +
                                               $"Temp Min: {t.temp_min} °C\n" +
                                               $"Descrição: {t.description} \n" +
                                               $"Velocidade do Vento: {t.speed} m/s\n" +
                                               $"Visibilidade: {t.visibility} metros";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de Previsão";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }
            }
            catch (Exception ex)
            {
                // Aqui cai a mensagem de "Cidade não encontrada" e outros erros
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
