const gamePlayContainerDiv = document.getElementById("game-play-container");
const menuDiv = document.getElementById("menu");
const startBtn = document.getElementById("start-btn");

// when clicked, closes the menu and opens the GamePlay
startBtn.addEventListener("click", () => {
  menuDiv.style.display = "none";
  gamePlayContainerDiv.style.display = "block";
  createUnityInstance(document.querySelector("#unity-canvas"), {
    arguments: [],
    dataUrl: "/Build/www_cockpit.data",
    frameworkUrl: "Build/www_cockpit.framework.js",
    codeUrl: "Build/www_cockpit.wasm",
    streamingAssetsUrl: "StreamingAssets",
    companyName: "DefaultCompany",
    productName: "WebTutorial1",
    productVersion: "0.1",
    // matchWebGLToCanvasSize: false, // Uncomment this to separately control WebGL canvas render size and DOM element size.
    // devicePixelRatio: 1, // Uncomment this to override low DPI rendering on high DPI displays.
  })
    .then((unityInstance) => {})
    .catch((message) => {
      alert(message);
    });
});

if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
  // Mobile device style: fill the whole browser client area with the game canvas:
  var meta = document.createElement("meta");
  meta.name = "viewport";
  meta.content =
    "width=device-width, height=device-height, initial-scale=1.0, user-scalable=no, shrink-to-fit=yes";
  document.getElementsByTagName("head")[0].appendChild(meta);

  let canvas = document.querySelector("#unity-canvas");
  canvas.style.width = "100%";
  canvas.style.height = "100%";
  canvas.style.position = "fixed";

  document.body.style.textAlign = "left";
}
