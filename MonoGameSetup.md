-MonoGames requires .Net and some native libraries
sudo pacman -S dotnet-sdk mono gtk-sharp-2
-MonoGames templates
dotnet new install MonoGame.Templates.CSharp
-it shows the templates (confirm it works)
dotnet new --list | grep MonoGame
-create new project
dotnet new mgdesktopgl -o SpaceInvaders
cd SpaceInvaders
dotnet run
-add Assets
mkdir Content; cd Content;
edit .csproj:
<ItemGroup>
    <MonoGameContentReference Include="Content/Content.mgcb"/>
</ItemGroup>
then:
dotnet mgcb init Content
dotnet mgcb add Content/name.png
...
dotnet mgcb build Content/Content.mgcb
