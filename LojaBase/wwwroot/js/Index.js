
const produtoContainer = document.getElementById('produto-container');

numProdutos();
let resizeTimeout;

window.addEventListener('resize', () =>
{
    clearTimeout(resizeTimeout);
    resizeTimeout = setTimeout(() => {
        numProdutos();
    }, 400);
});

function numProdutos()
{
    const produtos = Array.from(produtoContainer.children);
    let firstItem = produtos[0].offsetTop;
    console.log(firstItem);

    for (let i = 0; i < produtos.length; i++) {

        if (produtos[i].offsetTop > firstItem) {
            produtos[i].hidden = true;
        }
        else
        {
            produtos[i].hidden = false;
        }
    }
}

/*
const produtoContainer = document.getElementById('produto-container');

function numProdutos() {
  const produtos = Array.from(produtoContainer.children);
  if (produtos.length === 0) return;

  const firstItem = produtos[0].offsetTop;

  produtos.forEach(produto => {
    produto.hidden = produto.offsetTop > firstItem;
  });
}

const resizeObserver = new ResizeObserver(() => {
  requestAnimationFrame(numProdutos);
});

resizeObserver.observe(produtoContainer);

// initial call
numProdutos();
*/