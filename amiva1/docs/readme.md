Norint pagaminti pdf dokumentą iš markdown failo, reikia turėti `pandoc`, `biber` ir `biblatex` paketus.  
Terminale esant `/docs` folderyje ir įvykdžius  
`pandoc --template template.latex <folder>/<file.md> -o <folder>/<file.pdf> --pdf-engine=xelatex`  
kur `<folder>`yra folderio pavadinimas, o `<file.md>` - failas, iš kurio generuojoamas dokumnetas, pdf dokumentas turėtų būti sugeneruotas

1 laboratorinis darbas:  
`pandoc --template template.latex lab1/lab1.md -o lab1/lab1.pdf --pdf-engine=xelatex`  

2 laboratorinis darbas:  
`pandoc --template template.latex lab2/lab2.md -o lab2/lab2.pdf --pdf-engine=xelatex`  