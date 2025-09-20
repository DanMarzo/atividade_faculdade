const inputEmail = document.getElementById('email');
const alertEmail = document.getElementById('emailAlert');

function isValidEmail(email) {
    const regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regexEmail.test(email);
}

inputEmail.addEventListener('input', e => {
    alertEmail.innerHTML = !isValidEmail(e.target.value) ? "Email inválido. Digite no formato email@email.com" : "";
});
