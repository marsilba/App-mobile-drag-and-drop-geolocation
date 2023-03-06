$.validator.methods.range = function (value, element, param) { 
    var globalizedValue = value.replace(".", ""); 
    globalizedValue = globalizedValue.replace(",", "."); 
    return this.optional(element) || 
        (globalizedValue >= param[0] && globalizedValue <= param[1]); 
};

$.validator.methods.number = function (value, element) { 
    return this.optional(element) || 
        /^-?(?:\d+|\d{1,3}(?:[\s\,]\d{3})+)(?:[\,]\d+)?$/ .test(value); 
};

// http://stackoverflow.com/questions/6906725/unobtrusive-validation-in-chrome-wont-validate-with-dd-mm-yyyy
// sobrescrevendo o método para contornar erro no Chrome
$.validator.methods["date"] = function (value, element) {
    var dia = value.split('/')[0];
    var mes = value.split('/')[1];
    var ano = value.split('/')[2];

    value = mes + "/" + dia + "/" + ano;
    
    return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
};

// Leia mais em: ASP.NET MVC: Manipulando valores monetários 
// http://www.devmedia.com.br/asp-net-mvc-manipulando-valores-monetarios/28907#ixzz2vPA2uTY8