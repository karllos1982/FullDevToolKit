window.maskedInput = {
    initConditional: function (elementId, conditionalMasks, dotnetHelper) {
        var input = document.getElementById(elementId);
        if (!input) return;

        input.addEventListener("input", function () {
            var digits = input.value.replace(/\D/g, "");
            var mask = conditionalMasks[digits.length];

            if (mask) {
                var im = new Inputmask(mask);
                im.mask(input);
            }

            // envia valor formatado e cru
            dotnetHelper.invokeMethodAsync("UpdateValues", input.value, digits);
        });
    }
};