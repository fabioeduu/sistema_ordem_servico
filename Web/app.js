// Configuração da API
const API_URL = 'http://localhost:5187/api';

// Função para trocar de aba
function switchTab(tabName) {
    // Remover active de todas as tabs
    document.querySelectorAll('.tab').forEach(tab => tab.classList.remove('active'));
    document.querySelectorAll('.tab-content').forEach(content => content.classList.remove('active'));
    
    // Adicionar active na tab clicada
    event.target.classList.add('active');
    document.getElementById(`${tabName}-tab`).classList.add('active');
    
    // Carregar dados da aba
    if (tabName === 'clientes') {
        listarClientes();
    } else if (tabName === 'veiculos') {
        listarVeiculos();
        carregarClientesNoSelect();
    } else if (tabName === 'ordens') {
        listarOrdensServico();
        carregarVeiculosNoSelect();
    }
}

// Função para mostrar mensagem
function showMessage(elementId, message, isError = false) {
    const messageDiv = document.getElementById(elementId);
    messageDiv.innerHTML = `
        <div class="alert ${isError ? 'alert-error' : 'alert-success'}">
            ${message}
        </div>
    `;
    setTimeout(() => {
        messageDiv.innerHTML = '';
    }, 5000);
}

// CLIENTES
async function criarCliente(event) {
    event.preventDefault();
    
    const cliente = {
        nome: document.getElementById('cliente-nome').value,
        cpf: document.getElementById('cliente-cpf').value,
        telefone: document.getElementById('cliente-telefone').value,
        email: document.getElementById('cliente-email').value,
        endereco: document.getElementById('cliente-endereco').value
    };

    try {
        const response = await fetch(`${API_URL}/clientes`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(cliente)
        });

        if (response.ok) {
            showMessage('cliente-message', '✅ Cliente cadastrado com sucesso!');
            document.getElementById('cliente-form').reset();
            listarClientes();
        } else {
            const error = await response.json();
            showMessage('cliente-message', `❌ Erro: ${error.mensagem || 'Erro ao cadastrar cliente'}`, true);
        }
    } catch (error) {
        showMessage('cliente-message', `❌ Erro de conexão: ${error.message}`, true);
    }
}

async function listarClientes() {
    const listDiv = document.getElementById('clientes-list');
    listDiv.innerHTML = '<div class="loading">Carregando clientes...</div>';

    try {
        const response = await fetch(`${API_URL}/clientes`);
        const clientes = await response.json();

        if (clientes.length === 0) {
            listDiv.innerHTML = `
                <div class="empty-state">
                    <div style="font-size: 4rem;">👤</div>
                    <h3>Nenhum cliente cadastrado</h3>
                    <p>Cadastre o primeiro cliente usando o formulário acima</p>
                </div>
            `;
            return;
        }

        listDiv.innerHTML = clientes.map(cliente => `
            <div class="list-item">
                <h3>👤 ${cliente.nome}</h3>
                <p><strong>CPF:</strong> ${cliente.cpf}</p>
                <p><strong>Telefone:</strong> ${cliente.telefone || 'Não informado'}</p>
                <p><strong>Email:</strong> ${cliente.email || 'Não informado'}</p>
                <p><strong>Endereço:</strong> ${cliente.endereco || 'Não informado'}</p>
            </div>
        `).join('');
    } catch (error) {
        listDiv.innerHTML = `<div class="alert alert-error">❌ Erro ao carregar clientes: ${error.message}</div>`;
    }
}

// VEÍCULOS

async function carregarClientesNoSelect() {
    const select = document.getElementById('veiculo-cliente');
    
    try {
        const response = await fetch(`${API_URL}/clientes`);
        const clientes = await response.json();

        select.innerHTML = '<option value="">Selecione um cliente</option>' +
            clientes.map(c => `<option value="${c.id}">${c.nome} - ${c.cpf}</option>`).join('');
    } catch (error) {
        console.error('Erro ao carregar clientes:', error);
    }
}

async function criarVeiculo(event) {
    event.preventDefault();
    
    const veiculo = {
        clienteId: parseInt(document.getElementById('veiculo-cliente').value),
        placa: document.getElementById('veiculo-placa').value,
        marca: document.getElementById('veiculo-marca').value,
        modelo: document.getElementById('veiculo-modelo').value,
        ano: parseInt(document.getElementById('veiculo-ano').value),
        cor: document.getElementById('veiculo-cor').value
    };

    try {
        const response = await fetch(`${API_URL}/veiculos`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(veiculo)
        });

        if (response.ok) {
            showMessage('veiculo-message', '✅ Veículo cadastrado com sucesso!');
            document.getElementById('veiculo-form').reset();
            listarVeiculos();
        } else {
            const error = await response.json();
            showMessage('veiculo-message', `❌ Erro: ${error.mensagem || 'Erro ao cadastrar veículo'}`, true);
        }
    } catch (error) {
        showMessage('veiculo-message', `❌ Erro de conexão: ${error.message}`, true);
    }
}

async function listarVeiculos() {
    const listDiv = document.getElementById('veiculos-list');
    listDiv.innerHTML = '<div class="loading">Carregando veículos...</div>';

    try {
        const response = await fetch(`${API_URL}/veiculos`);
        const veiculos = await response.json();

        if (veiculos.length === 0) {
            listDiv.innerHTML = `
                <div class="empty-state">
                    <div style="font-size: 4rem;">🚗</div>
                    <h3>Nenhum veículo cadastrado</h3>
                    <p>Cadastre o primeiro veículo usando o formulário acima</p>
                </div>
            `;
            return;
        }

        listDiv.innerHTML = veiculos.map(veiculo => `
            <div class="list-item">
                <h3>🚗 ${veiculo.placa} - ${veiculo.marca} ${veiculo.modelo}</h3>
                <p><strong>Ano:</strong> ${veiculo.ano}</p>
                <p><strong>Cor:</strong> ${veiculo.cor || 'Não informado'}</p>
                <p><strong>Proprietário:</strong> ${veiculo.nomeCliente || 'Não informado'}</p>
            </div>
        `).join('');
    } catch (error) {
        listDiv.innerHTML = `<div class="alert alert-error">❌ Erro ao carregar veículos: ${error.message}</div>`;
    }
}

// ORDENS DE SERVIÇO

async function carregarVeiculosNoSelect() {
    const select = document.getElementById('ordem-veiculo');
    
    try {
        const response = await fetch(`${API_URL}/veiculos`);
        const veiculos = await response.json();

        select.innerHTML = '<option value="">Selecione um veículo</option>' +
            veiculos.map(v => `<option value="${v.id}">${v.placa} - ${v.marca} ${v.modelo}</option>`).join('');
    } catch (error) {
        console.error('Erro ao carregar veículos:', error);
    }
}

async function criarOrdemServico(event) {
    event.preventDefault();
    
    // Converter número do status para o nome do enum
    const statusValue = parseInt(document.getElementById('ordem-status').value);
    const statusMap = {
        1: 'Aberta',
        2: 'EmAndamento',
        3: 'Aguardando',
        4: 'Concluida',
        5: 'Cancelada'
    };
    
    const ordem = {
        veiculoId: parseInt(document.getElementById('ordem-veiculo').value),
        descricao: document.getElementById('ordem-descricao').value,
        observacoes: document.getElementById('ordem-observacoes').value,
        valorTotal: parseFloat(document.getElementById('ordem-valor').value),
        status: statusMap[statusValue]
    };

    try {
        const response = await fetch(`${API_URL}/ordensservico`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(ordem)
        });

        if (response.ok) {
            showMessage('ordem-message', '✅ Ordem de serviço criada com sucesso!');
            document.getElementById('ordem-form').reset();
            listarOrdensServico();
        } else {
            const error = await response.json();
            showMessage('ordem-message', `❌ Erro: ${error.mensagem || 'Erro ao criar ordem de serviço'}`, true);
        }
    } catch (error) {
        showMessage('ordem-message', `❌ Erro de conexão: ${error.message}`, true);
    }
}

async function fecharOrdemServico(id) {
    if (!confirm('Deseja realmente fechar esta ordem de serviço?')) {
        return;
    }

    try {
        const response = await fetch(`${API_URL}/ordensservico/${id}/fechar`, {
            method: 'PUT'
        });

        if (response.ok) {
            showMessage('ordem-message', '✅ Ordem de serviço fechada com sucesso!');
            listarOrdensServico();
        } else {
            const error = await response.json();
            showMessage('ordem-message', `❌ Erro: ${error.mensagem || 'Erro ao fechar ordem'}`, true);
        }
    } catch (error) {
        showMessage('ordem-message', `❌ Erro de conexão: ${error.message}`, true);
    }
}

async function listarOrdensServico() {
    const listDiv = document.getElementById('ordens-list');
    listDiv.innerHTML = '<div class="loading">Carregando ordens de serviço...</div>';

    try {
        const response = await fetch(`${API_URL}/ordensservico`);
        const ordens = await response.json();

        if (ordens.length === 0) {
            listDiv.innerHTML = `
                <div class="empty-state">
                    <div style="font-size: 4rem;">🔧</div>
                    <h3>Nenhuma ordem de serviço</h3>
                    <p>Crie a primeira ordem de serviço usando o formulário acima</p>
                </div>
            `;
            return;
        }

        listDiv.innerHTML = ordens.map(ordem => {
            const dataAbertura = new Date(ordem.dataAbertura).toLocaleDateString('pt-BR');
            const dataFechamento = ordem.dataFechamento ? 
                new Date(ordem.dataFechamento).toLocaleDateString('pt-BR') : 'Em aberto';
            
            const statusClass = ordem.status.toLowerCase().replace(' ', '');
            
            // Ícones e textos para cada status
            let statusIcon = '🟡';
            let statusText = ordem.status;
            if (ordem.status === 'Concluida' || ordem.status === 'Concluída') {
                statusIcon = '🟢';
                statusText = 'Concluída';
            } else if (ordem.status === 'EmAndamento' || ordem.status === 'Em Andamento') {
                statusIcon = '🔵';
                statusText = 'Em Andamento';
            } else if (ordem.status === 'Aguardando') {
                statusIcon = '🟠';
            } else if (ordem.status === 'Cancelada') {
                statusIcon = '🔴';
            } else if (ordem.status === 'Aberta') {
                statusIcon = '🟡';
            }
            
            return `
                <div class="list-item">
                    <h3>🔧 Ordem #${ordem.id}</h3>
                    <p><span class="status-badge status-${statusClass}">${statusIcon} ${statusText}</span></p>
                    <p><strong>Veículo:</strong> ${ordem.placaVeiculo || 'N/A'} - ${ordem.modeloVeiculo || 'N/A'}</p>
                    <p><strong>Descrição:</strong> ${ordem.descricao}</p>
                    <p><strong>Observações:</strong> ${ordem.observacoes || 'Nenhuma'}</p>
                    <p><strong>💰 Valor Total:</strong> <span style="color: #28a745; font-size: 1.2rem; font-weight: bold;">R$ ${ordem.valorTotal.toFixed(2).replace('.', ',')}</span></p>
                    <p><strong>📅 Data Abertura:</strong> ${dataAbertura}</p>
                    <p><strong>📅 Data Fechamento:</strong> ${dataFechamento}</p>
                    ${ordem.status === 'Aberta' ? 
                        `<button class="btn" onclick="fecharOrdemServico(${ordem.id})" style="margin-top: 10px;">
                            ✔️ Fechar Ordem
                        </button>` : ''}
                </div>
            `;
        }).join('');
    } catch (error) {
        listDiv.innerHTML = `<div class="alert alert-error">❌ Erro ao carregar ordens: ${error.message}</div>`;
    }
}

// Formatar data
function formatarData(dataString) {
    if (!dataString) return 'N/A';
    const data = new Date(dataString);
    return data.toLocaleDateString('pt-BR') + ' ' + data.toLocaleTimeString('pt-BR');
}

// Carregar dados iniciais ao abrir a página
window.addEventListener('DOMContentLoaded', () => {
    listarClientes();
});
