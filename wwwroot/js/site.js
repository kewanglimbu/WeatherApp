
// convertion of temperature to different units

var temperatureConversionSelect = document.getElementById("temperatureConversion");
var convertedTemperatureDiv = document.getElementById("convertedTemperature");

temperatureConversionSelect.addEventListener("change", function () {
    var selectedValue = temperatureConversionSelect.value;
    var temperature = parseFloat("@Model.Temperature");

    if (selectedValue === "kelvinToCelsius") {
        var celsiusTemperature = temperature - 273.15;
        convertedTemperatureDiv.textContent = "Temperature in Celsius: " + celsiusTemperature.toFixed(2) + " °C";
    } else if (selectedValue === "celsiusToFahrenheit") {
        var celsiusTemperature = temperature - 273.15;
        var FahrenTemperature = (celsiusTemperature * 9 / 5) + 32;
        convertedTemperatureDiv.textContent = "Temperature in Fahrenheit: " + FahrenTemperature.toFixed(2) + " °F";
    }
});

/*
Note:
1. show the #convertedTemperature element only when a conversion option is selected from the <select> dropdown.

Code explain:
""""
 Event listener is added to the change event of the #temperatureConversion element.
When a different option is selected from the dropdown, the event is triggered.
If a valid conversion option (other than the default "Select") is selected, the #convertedTemperature element's 
display style property is set to "block", making it visible. Otherwise, if the "Select" option is chosen, 
the element is hidden by setting its display property to "none".
Now, the #convertedTemperature element will be shown when a conversion option is selected from the dropdown,
and it will be hidden when the "Select" option is chosen.
 
"""
*/
document.addEventListener("DOMContentLoaded", function () {
    var convertedTemperature = document.getElementById("convertedTemperature");
    var temperatureConversion = document.getElementById("temperatureConversion");

    temperatureConversion.addEventListener("change", function () {
        if (this.value !== "") {
            convertedTemperature.style.display = "block";
        } else {
            convertedTemperature.style.display = "none";
        }
    });
});

// current date and time in a localized format.

var dateTimeDisplay = document.getElementById("dateTimeDisplay");
var currentDateTime = new Date();
var formattedDateTime = currentDateTime.toLocaleString();

dateTimeDisplay.textContent = formattedDateTime;