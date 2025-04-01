// This is for Alerts returned from Controllers
setTimeout(function () {
    let alert = document.querySelector(".alert");
    if (alert) {
        alert.classList.remove("show");
        alert.classList.add("fade");
        setTimeout(() => alert.remove(), 500);
    }
}, 5000);

document.addEventListener("DOMContentLoaded", function () {
    let modals = ["addStaffModal", "addCitizenModal"];

    modals.forEach(modalId => {
        let modal = document.getElementById(modalId);

        if (modal) {
            modal.addEventListener("shown.bs.modal", function () {
                let phoneForm = modal.querySelector("form");
                let phoneInput = modal.querySelector("#PhoneNumber");
                let errorText = modal.querySelector("#phoneError");

                if (!phoneForm || !phoneInput || !errorText) return;

                let validatePhone = function (event) {
                    let pattern = /^[0-9]{3} [0-9]{3} [0-9]{4}$/;
                    if (!pattern.test(phoneInput.value.trim())) {
                        event.preventDefault();
                        errorText.style.display = "block";
                    } else {
                        errorText.style.display = "none";
                    }
                };

                let hideErrorOnInput = function () {
                    let pattern = /^[0-9]{3} [0-9]{3} [0-9]{4}$/;
                    if (pattern.test(phoneInput.value.trim())) {
                        errorText.style.display = "none";
                    }
                };

                phoneForm.addEventListener("submit", validatePhone);
                phoneInput.addEventListener("input", hideErrorOnInput);

                modal.addEventListener("hidden.bs.modal", function () {
                    phoneForm.reset();
                    errorText.style.display = "none";
                    phoneForm.removeEventListener("submit", validatePhone);
                    phoneInput.removeEventListener("input", hideErrorOnInput);
                });
            });
        }
    });
});
