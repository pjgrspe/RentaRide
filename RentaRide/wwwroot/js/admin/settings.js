function initEditButton() {
    let editButton = document.getElementById('editButton');
    let editButtons = document.getElementById('editButtons');
    let formElements = document.getElementById('accountForm').elements;

    if (editButton && editButtons) {
        editButton.classList.remove('d-none');
        editButtons.classList.add('d-none');

        for (let i = 0; i < formElements.length; i++) {
            formElements[i].disabled = true;
        }
    } else {
        console.error("Edit button or edit buttons container not found in the DOM.");
    }
}

function toggleEdit() {
    let formElements = document.getElementById('accountForm').elements;
    for (let i = 0; i < formElements.length; i++) {
        formElements[i].disabled = !formElements[i].disabled;
    }
    document.getElementById('editButton').classList.add('d-none');
    document.getElementById('editButtons').classList.remove('d-none');
}

function saveChanges() {
    // Function to save changes made in the settings form
    // Add your save logic here
    alert('Changes saved successfully!');
    document.getElementById('editButton').classList.remove('d-none');
    document.getElementById('editButtons').classList.add('d-none');

    let formElements = document.getElementById('accountForm').elements;
    for (let i = 0; i < formElements.length; i++) {
        formElements[i].disabled = true;
    }
}

function cancelChanges() {
    // Function to cancel changes and revert to original state
    document.getElementById('editButton').classList.remove('d-none');
    document.getElementById('editButtons').classList.add('d-none');

    let formElements = document.getElementById('accountForm').elements;
    for (let i = 0; i < formElements.length; i++) {
        formElements[i].disabled = true;
    }
    // Revert changes (if needed, store the original values and reset them here)
}

document.addEventListener("DOMContentLoaded", function () {
    initEditButton();
});
