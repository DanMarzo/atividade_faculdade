function start() {
    const isEmpty = document.currentScript.getAttribute("data-empty")
    if (isEmpty && isEmpty.toLowerCase() == 'true') {
        alert("Nenhuma transação foi localizada!")
        location.href = "/"
    }
}

start();