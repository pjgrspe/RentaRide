/*THIS IS FOR THE MULTI-STEP FORM*/
var currentStep = 1;
var updateProgressBar;

function displayStep(stepNumber) {
    if (stepNumber >= 1 && stepNumber <= 4) {
        $(".step-" + currentStep).hide();
        $(".step-" + stepNumber).show();
    }
}

$(document).ready(function () {
    $("#multi-step-form").find(".step").slice(1).hide();

    $(".next-step").click(function () {
        if (currentStep < 4) {
            $(".step-" + currentStep).addClass(
                "animate__animated animate__fadeOutLeft"
            );
            currentStep++;
            setTimeout(function () {
                $(".step").removeClass("animate__animated animate__fadeOutLeft animate__fadeInLeft").hide();
                $(".step-" + currentStep)
                    .show()
                    .addClass("animate__animated animate__fadeInRight");
            }, 500);
        }
    });

    $(".prev-step").click(function () {
        if (currentStep >= 1) {
            $(".step-" + currentStep).addClass(
                "animate__animated animate__fadeOutRight"
            );
            currentStep--;
            setTimeout(function () {
                $(".step")
                    .removeClass("animate__animated animate__fadeOutRight animate__fadeInRight")
                    .hide();
                $(".step-" + currentStep)
                    .show()
                    .addClass("animate__animated animate__fadeInLeft");
            }, 500);
        }
    });
});

/*ADD SCRIPTS BELOW*/