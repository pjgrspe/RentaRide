function updatePriceInput(value, inputId) {
    document.getElementById(inputId).value = `P${value}`;
}

//EDIT LOGS MODAL
function openModalEditListing() {
    $('#editListingModal').modal('show');
}

function closeModalEditListing() {
    $('#editListingModal').modal('hide');
}

function openModalAddListing(){
    $('#addListingModal').modal('show');
}

function closeModalAddListing() {
    $('#addListingModal').modal('show');
}