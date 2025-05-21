const proxImgBtn = document.getElementById("proxImg");
const imgAntBtn = document.getElementById("imgAnt");
let image = 0

proxImgBtn.onclick = function()
{
    image = image + 1;
    console.log("próxima imagem");
    console.log(image);
}

imgAntBtn.onclick = function()
{
    image = image -1;
    console.log("Imagem anterior");
    console.log(image);
}