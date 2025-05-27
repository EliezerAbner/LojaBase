const cep = document.getElementById("cepTxt");
const logradouro = document.getElementById("logradouroTxt");
const bairro = document.getElementById("bairroTxt");
const localidade = document.getElementById("localidadeTxt");


cep.onblur = () => 
{
    let cepValue = Number(cep.value);

    console.log(cepValue);
    const url = `http://localhost:5033/admin/usuarios/buscaCep?cepTxt=${cepValue}`;

    fetch(url)
    .then(response => {
        if (!response.ok) 
        {
            throw new Error("Erro ao consultar o CEP.");
        }
        return response.json();
    })
    .then(data => 
        {
            if (!data.erro) 
            {
                cep.value = data.cep;
                logradouro.value = data.logradouro;
                bairro.value = data.bairro;
                localidade.value = data.localidade;
            }
    })
    .catch(error => {
        console.error(error);
    });
}