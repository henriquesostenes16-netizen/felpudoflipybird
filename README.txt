Felpudo Voador: Batalha contra o Chefe — Jogo 2D
[link do Unity Play]

Jogo 2D de ação feito na Unity 6 com sprites e geração de inimigos por código. O Felpudo voa pela tela, atira nas lesmas-balão que vêm em sua direção e, depois de um tempo, enfrenta um chefe gigante. Derrotando o chefe, aparece a mensagem VOCÊ VENCEU!

## Objetivo
- Controlar o Felpudo voando e desviando dos inimigos.
- Atirar nas lesmas-balão para eliminá-las.
- Sobreviver até a chegada do chefe.
- Derrotar o chefe e chegar à tela de vitória.

## Como jogar
Abra o link do jogo no Unity Play (ou o projeto/executável) e use os controles abaixo.

| Ação | Controle |
|------|----------|
| Pular / bater asas | Botão esquerdo do mouse |
| Atirar | Barra de espaço ou botão direito do mouse |

Para jogar de novo, atualize a página.

## Elementos do jogo

| Elemento | Descrição |
|----------|-----------|
| Felpudo | Herói controlado pelo jogador, que voa e atira |
| Lesmas-balão | Inimigos que entram pela borda direita e miram no Felpudo; morrem com 1 tiro e piscam em vermelho ao serem atingidas |
| Chefe | Aparece depois de 30 segundos (valor padrão), para no lado direito da tela e sobe e desce; aguenta 20 tiros |
| Tiro | Projétil que sai do Felpudo, com intervalo entre disparos |
| Tela de vitória | Texto VOCÊ VENCEU! no centro da tela quando o chefe é derrotado |

## Sequência do jogo
1. As lesmas-balão entram pela direita, uma a uma, e voam na direção do Felpudo.
2. O jogador atira para eliminá-las.
3. Passado o tempo definido, todas as lesmas somem.
4. O chefe entra pela direita, para na posição X = 6 e se move para cima e para baixo.
5. O jogador acerta 20 tiros no chefe.
6. O chefe é destruído e aparece VOCÊ VENCEU!

## Como foi feito
- **Engine:** Unity 6 [versão], template 2D
- **Linguagem:** C#
- **Scripts:**
  - `GeradorInimigos`: cria as lesmas, controla o tempo e inicia a fase do chefe
  - `AtiraTiro`: faz o Felpudo atirar
  - `Tiro`: movimento e dano do projétil
  - `Vida`: vida e efeito de dano (piscar em vermelho)
  - `Chefe`: movimento do chefe
- **Criação por código:** inimigos, tiros e chefe são criados em tempo de execução, sem prefab
- **Publicação:** build WebGL no Unity Play

## Configurações (Inspector > GeradorInimigos)
- **Tempo Ate Chefe:** quando o chefe aparece
- **Vida Chefe:** quantos tiros o chefe aguenta
- **Escala Chefe:** tamanho do chefe
- **Sprite Inimigo / Sprite Chefe:** imagens usadas

## Demonstração
O vídeo ([link do YouTube]) mostra uma partida completa, do início até a vitória.

## Regras resumidas
- O botão esquerdo do mouse faz o Felpudo pular; o direito (ou a barra de espaço) atira.
- Cada lesma morre com 1 tiro.
- O chefe só aparece depois do tempo definido.
- Ainda não há derrota: lesmas e chefe não machucam o Felpudo.

## Créditos
Trabalho desenvolvido por Sostenes.

Sprites do Felpudo e das lesmas-balão: [origem do material, por exemplo, prática Lab da UNEB].