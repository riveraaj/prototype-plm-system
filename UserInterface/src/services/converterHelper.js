export function formatPickerDate(pickerDate) {
    if (!pickerDate) return null;

    //const { year, month, day } = pickerDate;
    const { month, day } = pickerDate;

    // Ajustar el año para obtener solo los dos últimos dígitos
    //const shortYear = year.toString().slice(-2);

    // Asegurar que el mes y el día tengan dos dígitos
    const formattedMonth = month.toString().padStart(2, '0');
    const formattedDay = day.toString().padStart(2, '0');

    // Crear la cadena en el formato deseado
    return `${formattedMonth}-${formattedDay}`;
}

export function formatPickerDateComplete(pickerDate) {
    if (!pickerDate) return null;

    //const { year, month, day } = pickerDate;
    const { month, day, year } = pickerDate;

    // Asegurar que el mes y el día tengan dos dígitos
    const formattedMonth = month.toString().padStart(2, '0');
    const formattedDay = day.toString().padStart(2, '0');

    // Crear la cadena en el formato deseado
    return `${year}-${formattedMonth}-${formattedDay}`;
}

// Función para verificar si el valor es una cadena con formato de fecha ISO
function isISODateString(value) {
    const isoDateRegex = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}$/;
    return typeof value === 'string' && isoDateRegex.test(value);
}

// Función para convertir una cadena ISO a fecha con formato "MM/DD/YY"
export function formatDate(value) {
    const date = new Date(value);
    if (isNaN(date.getTime())) return value;

    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear().toString().slice(-2);

    return `${month}/${day}/${year}`;
}

// Función para formatear fechas en un objeto
export function formatDatesInObject(obj) {
    const formattedObj = { ...obj };

    Object.keys(formattedObj).forEach(key => {
        if (isISODateString(formattedObj[key])) {
            formattedObj[key] = formatDate(formattedObj[key]);
        }
    });

    return formattedObj;
}

// Función para formatear fechas en un array de objetos
export function formatDatesInArray(array) {
    return array.map(item => formatDatesInObject(item));
}

export function convertFileToBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = (error) => reject(error);
    });
}
