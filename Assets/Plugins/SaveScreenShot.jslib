mergeInto(LibraryManager.library, {
  SaveScreenShotJS: function (base64Ptr) {
    const imageDataUrl = UTF8ToString(base64Ptr);
    const base64 = imageDataUrl.split(',')[1];
    const binaryString = atob(base64);
    const length = binaryString.length;
    const bytes = new Uint8Array(length);
    for (let i = 0; i < length; i++) {
      bytes[i] = binaryString.charCodeAt(i);
    }

    try {
      const blob = new Blob([bytes.buffer], { type: 'image/png' });
      const objectUrl = URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.download = "ScreenShot.png";
      a.href = objectUrl;
      a.click();
      URL.revokeObjectURL(objectUrl);
    } catch (e) {
      console.error("SaveScreenShotJS error:", e);
    }
  },
});
