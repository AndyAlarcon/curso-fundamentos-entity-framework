using Microsoft.EntityFrameworkCore;
using proyectoef.Models;
namespace proyectoef;
public class TareasContext: DbContext
{
    public DbSet<Categoria> Categorias {get; set;}
    public DbSet<Tarea> Tareas {get; set;}
    public TareasContext(DbContextOptions<TareasContext> options) :base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria(){
            CategoriaId = Guid.Parse("5da37eb0-a965-4882-bcca-7c1669415b43"),
            Nombre = "Actividades pendientes",
            Peso = 20
        });
        categoriasInit.Add(new Categoria(){
            CategoriaId = Guid.Parse("5da37eb0-a965-4882-bcca-7c1669415b02"),
            Nombre = "Actividades personales",
            Peso = 50
        });

        modelBuilder.Entity<Categoria>(categoria =>{
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);

            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

            categoria.Property(p => p.Descripcion).IsRequired(false);

            categoria.Property(p => p.Peso);

            categoria.HasData(categoriasInit);
        });

        List<Tarea> tareasInit = new List<Tarea>();
        
        tareasInit.Add(new Tarea(){
            TareaId = Guid.Parse("5da37eb0-a965-4882-bcca-7c1669415b10"),
            CategoriaId = Guid.Parse("5da37eb0-a965-4882-bcca-7c1669415b43"),
            PrioridadTarea = Prioridad.Media,
            Titulo = "Pago de servicios públicos",
            FechaCreacion = DateTime.Now
        });
        tareasInit.Add(new Tarea(){
            TareaId = Guid.Parse("5da37eb0-a965-4882-bcca-7c1669415b11"),
            CategoriaId = Guid.Parse("5da37eb0-a965-4882-bcca-7c1669415b02"),
            PrioridadTarea = Prioridad.Baja,
            Titulo = "Terminar de ver película en Netflix",
            FechaCreacion = DateTime.Now
        });

        modelBuilder.Entity<Tarea>(tarea => {
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaId);
            
            tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

            tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);

            tarea.Property(p => p.Descripcion).IsRequired(false);

            tarea.Property(p => p.PrioridadTarea);

            tarea.Property(p => p.FechaCreacion);

            tarea.Ignore(p => p.Resumen);

            tarea.HasData(tareasInit);
        });
    }
}