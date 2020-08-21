// Write your Javascript code.
function toDate(dateStr) {
    var parts = dateStr.split("/")
    return new Date(parts[2], parts[1] - 1, parts[0])
} 
function toDate02(dateStr) {

    var day;
    var parts1 = dateStr.split("/");
    day = parts1[2] + '/' + parts1[1] + '/' + parts1[0];
    return day;
} 
function toDateTime(dateStr) {
    var day;
    var parts = dateStr.split(" ")
    var date = parts[0]
    var time = parts[1]
    var parts1 = date.split("/");
    var parts2 = time.split(":");
    day = parts1[2] + '/' + parts1[1] + '/' + parts1[0] + ' ' + parts[1]
    return day;
}