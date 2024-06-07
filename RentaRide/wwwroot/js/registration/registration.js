/*THIS IS FOR THE MULTI-STEP FORM*/
var currentStep = 1;
var updateProgressBar;

function displayStep(stepNumber) {
    if (stepNumber >= 1 && stepNumber <= 4) {
        if (!$("#multi-step-form").valid()) {
            // If the form is invalid, don't proceed to the next step
            return;
        }

        $(".step-" + currentStep).hide();
        $(".step-" + stepNumber).show();
        currentStep = stepNumber;
        updateProgressBar();
    }
}

$(document).ready(function () {
    var currentStep = 1; // Initialize currentStep
    var isTransitioning = false; // Flag to prevent rapid transitions

    $("#multi-step-form").find(".step").slice(1).hide();

    $(".next-step").click(function () {
        if (isTransitioning) return; // If already transitioning, do nothing
        if (currentStep < 4) {
            // Check if the current step's form inputs are valid
            if (!$("#multi-step-form").valid()) {
                // If the form is invalid, don't proceed to the next step
                return;
            }

            console.log("Next step from step " + currentStep); // Debugging

            $(".step-" + currentStep).addClass("animate__animated animate__fadeOutLeft");
            isTransitioning = true; // Set flag to true to prevent further transitions
            currentStep++;
            setTimeout(function () {
                $(".step").removeClass("animate__animated animate__fadeOutLeft animate__fadeInLeft").hide();
                $(".step-" + currentStep)
                    .show()
                    .addClass("animate__animated animate__fadeInRight");
                updateProgressBar();
                isTransitioning = false; // Reset flag after transition
            }, 500);
        }
    });

    $(".prev-step").click(function () {
        if (isTransitioning) return; // If already transitioning, do nothing
        if (currentStep > 1) {
            console.log("Previous step from step " + currentStep); // Debugging

            $(".step-" + currentStep).addClass("animate__animated animate__fadeOutRight");
            isTransitioning = true; // Set flag to true to prevent further transitions
            currentStep--;
            setTimeout(function () {
                $(".step")
                    .removeClass("animate__animated animate__fadeOutRight animate__fadeInRight")
                    .hide();
                $(".step-" + currentStep)
                    .show()
                    .addClass("animate__animated animate__fadeInLeft");
                updateProgressBar();
                isTransitioning = false; // Reset flag after transition
            }, 500);
        }
    });

    function updateProgressBar() {
        var progressPercentage = ((currentStep - 1) / 3) * 100;
        $(".progress-bar").css("width", progressPercentage + "%");
        console.log("Progress: " + progressPercentage + "%"); // Debugging
    }

    $("#multi-step-form").on('keypress', function (e) {
        if (e.which === 13) { // Enter key pressed
            e.preventDefault(); // Prevent form submission
            if (currentStep < 4) {
                console.log("Enter pressed at step " + currentStep); // Debugging
                $(".next-step").click(); // Trigger click event of next-step button
            }
        }
    });
});



/*ADD SCRIPTS BELOW*/
