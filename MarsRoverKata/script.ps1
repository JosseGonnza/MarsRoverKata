$result = dotnet test

if ($result -like "*Total tests: *, Failed: 0, Skipped: 0*") {
    Write-Host "Todos los tests pasaron, haciendo commit..."
    git add .
    git commit -m "Todos los tests pasaron, haciendo commit automático"
} else {
    Write-Host "Algunos tests fallaron, haciendo reset de los cambios..."
    git reset --hard HEAD
}