 Insert into SelecoesCopa2018 (Selecao,Continente,NumeroParticipacoes, MelhorResultado)         
    Values ('Brasil','América do Sul',21, 'Campeã (1958, 1962, 1970, 1994 e 2002)')    


SELECT * from SelecoesCopa2018

Create procedure ObterTodasAsSelecoes      
as      
Begin      
    select Id, Selecao, Continente, NumeroParticipacoes, MelhorResultado
    from SelecoesCopa2018   
    order by MelhorResultado    
End


Create procedure IncluirSelecao
(        
    @Selecao VARCHAR(35),         
    @Continente VARCHAR(60),        
    @NumeroParticipacoes int,        
    @MelhorResultado VARCHAR(40)        
)        
as         
Begin         
    Insert into SelecoesCopa2018 (Selecao,Continente,NumeroParticipacoes, MelhorResultado)         
    Values (@Selecao,@Continente,@NumeroParticipacoes, @MelhorResultado)         
End



Create procedure AtualizarSelecao        
(        
   @Id INTEGER ,      
   @Selecao VARCHAR(35),         
   @Continente VARCHAR(60),        
   @NumeroParticipacoes int,        
   @MelhorResultado VARCHAR(40)        
)        
as        
begin        
   Update SelecoesCopa2018         
   set Selecao=@Selecao,        
   Continente=@Continente,        
   NumeroParticipacoes=@NumeroParticipacoes,      
   MelhorResultado=@MelhorResultado        
   where Id=@Id        
End



Create procedure ExcluirSelecao       
(        
   @Id int        
)        
as         
begin        
   Delete from SelecoesCopa2018 where Id=@Id        
End