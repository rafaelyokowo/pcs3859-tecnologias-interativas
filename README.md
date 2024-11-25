# PCS3859 - Tecnologias Interativas e Aplicações

<div style="text-align: justify"> 
As tecnologias educacionais como os recursos audiovisuais e a realidade virtual se apresentam como ferramentas inovadoras de ensino e aprendizagem em saúde podendo ser utilizadas para atualização e treinamento de profissionais de saúde. O desenvolvimento de ambientes virtuais interativos pode ser utilizado como instrumento de interação e coleta de dados, para complementar o treinamento de habilidades de entrevista e exame físico para graduandos e profissionais da área da saúde e para promover educação em saúde, a fim de exercitar as habilidades de aferição de parâmetros (pressão arterial, febre) relativos ao paciente.

<br>

Nesse sentido, este projeto, concebido em parceria com a Escola de Enfermagem da Universidade de São Paulo (EEUSP), busca desenvolver uma ferramenta educacional com o uso da realidade virtual para treinamento da medida da pressão arterial. O desenvolvimento de ferramenta educativa audiovisual e a realidade virtual pode ser utilizada como instrumento de ensino-aprendizagem, para atualização, treinamento e aperfeiçoamento de profissionais da área da saúde e para o cuidado de pacientes. Ela será implementada no engine Unity e utilizada no Meta Quest durante sua criação e seu uso em aulas.
</div>

## Implementação Técnica

<div style="text-align: justify"> 
Este projeto utiliza o Unity para a concepção do projeto e o Meta Quest para a realização de testes e uso final da aplicação. Todos os códigos foram escritos em C#.
</div>

## Fluxo de Experiência

<div style="text-align: justify"> 
O processo de aferição de pressão é dividido em duas etapas distintas:

- Primeiramente, realiza-se um questionário de anamnese. Nele, o profissional de saúde responde a perguntas referentes à coleta de informações do paciente e verificação dos pré-requisitos da aferição de pressão;
- Em seguida, inicia-se o procedimento de aferição em si. Para isso, posiciona-se o manguito no braço do paciente e infla-se o esfigmomanômetro. Durante o processo, serão reproduzidos os sons de Korotkoff para realizar a aferição da pressão.

O jogo começa com o usuário fora do consultório médico. Para iniciá-lo, é preciso pressionar o botão para abrir a porta da clínica que está sob uma flecha vermelha. Em seguida, passa-se à etapa de questionário de anamnese a ser realizada em um tablet presente sobre a mesa também indicado por uma flecha vermelha. Uma vez o questionário concluído, o usuário pode deixar o tablet na posição original e seguir para a aferição de pressão. Há um avatar posicionado ao lado da mesa, nele o manguito sera posicionado e uma interface de medição de pressão será mostrada.

Após a conclusão de todas as etapas, os usuários podem recomeçar a experiência.

</div>


