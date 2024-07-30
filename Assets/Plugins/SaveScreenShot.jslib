mergeInto(LibraryManager.library, {
  SaveScreenShotJS: function (base64Tex) {
    const image = Pointer_stringify(base64Tex);

    var bin = atob(image.replace(/^.*,/, ''));
    var buffer = new Uint8Array(bin.length);
    for (var i = 0; i < bin.length; i++) {
      buffer[i] = bin.charCodeAt(i);
    }

    try{
      var blob = new Blob([buffer.buffer], {
        type: 'image/png'
      });
    }catch (e){
      return;
    }

    var url = (window.URL);
    var dataUrl = url.createObjectURL(blob);

    var a = document.createElement('a');
    a.download = "ScreenShot.png";
    a.href = dataUrl;

    a.click();

  },
});
