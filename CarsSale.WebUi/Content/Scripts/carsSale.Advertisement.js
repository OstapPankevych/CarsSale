$(function() {
    var success = function(html) {
        $("results").html(html);
    }
});

document.addEventListener("DOMContentLoaded", () => {

    const success = data => {
        document.getElementById("results").innerHTML = data;
    };

    const unselectedValue = -1;
    const getRegion = () => {
        const e = document.getElementById("region");
        const regionId = e.options[e.selectedIndex].value;
        return regionId === unselectedValue ? null : { Id: Number(regionId), Name: "wedw" };
    }

    const { contentLoader, urls } = carsSale;
    const { searchAdvertisements: url } = urls;

    const options = {
        contentType: "application/json;",
        dataType: "json",
        beforeSend, success
    };
    contentLoader.init(url, options);

    const searchBtn = document.getElementById("search");
    searchBtn.addEventListener("click", () => {
        const data = {
            Region: getRegion()
        };
        console.log(data);
        contentLoader.loadContent(data);
    });
});