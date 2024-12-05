// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const alertContainer = document.getElementById('alert-container');
const alertDisplay = document.createElement('div');

async function addStudentBook(bookIsbn) {

    try {
        const response = await fetch('/StudentDashboard/AddStudentBook', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ bookIsbn: bookIsbn })
        });
        if (!response.ok) {
            const errorMessage = await response.text();
            throw new Error(errorMessage);
        }
        if (response.ok) {
            const successMessage = await response.text();

            alertDisplay.innerHTML = '';
            showDismissableAlert(successMessage, "success");
        }

    } catch (error) {

        alertDisplay.innerHTML = '';
        showDismissableAlert(error, "danger");
    }
}

function showDismissableAlert(message, type) {


    alertDisplay.className = `alert alert-${type} alert-dismissible fade show d-flex align-items-center`;
    alertDisplay.role = "alert";

    const icon = {
        success: '<i class="fa-solid fa-circle-check m-2"></i>',
        warning: '<i class="fa-solid fa-triangle-exclamation m-2"></i>',
        danger: '<i class="fa-solid fa-triangle-exclamation m-2"></i>'
    }[type];

    alertDisplay.innerHTML = `${icon}
                       <div><strong>${message}</strong></div>
                       <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>`;

    alertContainer.appendChild(alertDisplay);
}