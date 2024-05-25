/* ---------------------------------------------------
    LOADER SCRIPT
----------------------------------------------------- */
function loadMainContent() {
    // Show the spinner initially
    document.getElementById('loadingSpinner').style.display = 'block';

    // Hide the spinner and show the content after a delay
    $(document).ready(function () {
        setTimeout(function () {
            $('#loadingSpinner').hide();
            $('.load-content').show(); // Show the content
        }, 500); // Adjust the delay (in milliseconds) as needed
    });
}

// Example of calling the function when the DOM is fully loaded
document.addEventListener("DOMContentLoaded", function () {
    loadMainContent();
});