/** On va intitialiser les valeurs dont on a besoin */

/** Récupération de la valeur dans l'input */
const input = document.querySelector(".input");

/** Récupération du bouton récupérer la valeur */
const button = document.querySelector(".start-btn");

if (button) {
  button.addEventListener("click", () => {
    let inputValue = input.value;
    console.log(inputValue); // audrey

    // Stocker une chaîne de caractères
    localStorage.setItem("playerName", inputValue);
  });
}

/** Récupération du nom de l'utilisateur */

const span = document.querySelector(".h1_play span");
if (span) {
  span.textContent = localStorage.getItem("playerName") || "";
}
