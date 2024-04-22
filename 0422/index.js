init(document.getElementById('canvas1'), 1, 'gray')
init(document.getElementById('canvas2'), 5, 'red')

function init(canvas, size, color) {
  canvas.width = window.innerWidth
  canvas.height = window.innerHeight

  const { width, height } = canvas

  const contx = canvas.getContext('2d')

  let previousSize = size

  contx.lineWidth = 3
  contx.strokeStyle = color

  contx.translate(width / 2, height / 2)

  for (let i = 0; i < 20; i++) {

    contx.beginPath()
    contx.arc(0, 0, size, 0, Math.PI / 2)
    contx.stroke()

    const tempSize = previousSize
    previousSize = size
    size += tempSize

    contx.translate(-tempSize, 0)
    contx.rotate(-Math.PI / 2)
  }
}